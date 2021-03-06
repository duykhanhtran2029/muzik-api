using CoreLib.AudioProcessing;
using CoreLib.AudioProcessing.Server;
using Database.AudioFingerPrinting;
using System;
using System.Collections.Generic;


namespace CoreLib
{
    public class RecognizerHelper
	{
		/// <summary>
		/// <para>Audio processing.</para>
		/// <para>WARNING: Song must be sampled at 48000Hz!</para>
		/// </summary>
		/// <param name="path">Location of .wav audio file</param>
		public static List<TimeFrequencyPoint> Processing(byte[] input)
		{
			//Plan of audio processing
			//STEREO -> MONO -> LOW PASS -> DOWNSAMPLE -> HAMMING -> FFT

			#region STEREO

			var audio = AudioReader.GetSound(input);

			#endregion

			#region MONO

			if (audio.Channels == 2)  //MONO
				AudioProcessor.StereoToMono(audio);

			#endregion

			#region Short to Double

			double[] data = ShortArrayToDoubleArray(audio.Data);
			#endregion

			#region LOW PASS & DOWNSAMPLE

			var downsampledData = AudioProcessor.DownSample(data, Constants.DownSampleCoef, audio.SampleRate); //LOWPASS + DOWNSAMPLE
			data = null; //release memory
			#endregion

			#region HAMMING & FFT
			//apply FFT at every 1024 samples
			//get 512 bins 
			//of frequencies 0 - 6 kHZ
			//bin size of ~ 11,7 Hz

			int bufferSize = Constants.WindowSize / Constants.DownSampleCoef; //default: 4096/4 = 1024
			return CreateTimeFrequencyPoints(bufferSize, downsampledData, sensitivity: 1);
			#endregion
		}

		/// <summary>
		/// Applies Hamming window and then FFT at every <c>bufferSize</c> number of samples.
		/// Filters out strongest bins and creates Time-frequency points that are ordered. Primarly by time, secondary by frequency.
		/// Both in ascending manner.
		/// </summary>
		/// <param name="bufferSize">Size of a window FFT will be applied to.</param>
		/// <param name="data">Data FFT will be applied to.</param>
		/// <returns></returns>
		public static List<TimeFrequencyPoint> CreateTimeFrequencyPoints(int bufferSize, double[] data, double sensitivity = 0.9)
		{
			List<TimeFrequencyPoint> TimeFrequencyPoitns = new List<TimeFrequencyPoint>();
			double[] HammingWindow = AudioProcessor.GenerateHammingWindow(bufferSize);
			double Avg = 0d;// = GetBinAverage(data, HammingWindow);

			int offset = 0;
			var sampleData = new double[bufferSize * 2]; //*2  because of Re + Im
			uint AbsTime = 0;
			while (offset < data.Length)
			{
				if (offset + bufferSize < data.Length)
				{
					for (int i = 0; i < bufferSize; i++) //setup for FFT
					{
						sampleData[i * 2] = data[i + offset] * HammingWindow[i];
						sampleData[i * 2 + 1] = 0d;
					}

					FastFourierTransformation.FFT(sampleData);
					double[] maxs =
					{
						GetStrongestBin(data, 0, 10),
						GetStrongestBin(data, 10, 20),
						GetStrongestBin(data, 20, 40),
						GetStrongestBin(data, 40, 80),
						GetStrongestBin(data, 80, 160),
						GetStrongestBin(data, 160, 512),
					};


					for (int i = 0; i < maxs.Length; i++)
					{
						Avg += maxs[i];
					}

					Avg /= maxs.Length;
					//get doubles of frequency and time 
					RegisterTFPoints(sampleData, Avg, AbsTime, ref TimeFrequencyPoitns, sensitivity);

				}

				offset += bufferSize;
				AbsTime++;
			}

			return TimeFrequencyPoitns;
		}

		/// <summary>
		/// Filter outs the strongest bins of logarithmically scaled parts of bins. Chooses the strongest and remembers it if its value is above average. Those points are
		/// chornologically added to the <c>timeFrequencyPoints</c> List.
		/// </summary>
		/// <param name="data">bins to choose from, alternating real and complex values as doubles. Must contain 512 complex values</param>
		/// <param name="average">Limit that separates weak spots from important ones.</param>
		/// <param name="absTime">Absolute time in the song.</param>
		/// <param name="timeFrequencyPoitns">List to add points to.</param>
		public static void RegisterTFPoints(double[] data, in double average, in uint absTime, ref List<TimeFrequencyPoint> timeFrequencyPoitns, double coefficient = 0.9)
		{
			int[] BinBoundries =
			{
				//low   high
				0 , 10,
				10, 20,
				20, 40,
				40, 80,
				80, 160,
				160,512
			};

			//loop through logarithmically scalled sections of bins
			for (int i = 0; i < BinBoundries.Length / 2; i++)
			{
				//get strongest bin from a section if its above average
				var idx = GetStrongestBinIndex(data, BinBoundries[i * 2], BinBoundries[i * 2 + 1], average, coefficient);
				if (idx != null)
				{
					//idx is divided by 2 because of (Re + Im)
					timeFrequencyPoitns.Add(new TimeFrequencyPoint(absTime, (uint)idx / 2));
				}
			}
		}

		/// <summary>
		/// Returns normalized value of the strongest bin in given bounds
		/// </summary>
		/// <param name="bins">Complex values alternating Real and Imaginary values</param>
		/// <param name="from">lower bound</param>
		/// <param name="to">upper bound</param>
		/// <returns>Normalized value of the strongest bin</returns>
		public static double GetStrongestBin(double[] bins, int from, int to)
		{
			var max = double.MinValue;
			for (int i = from; i < to; i++)
			{
				var normalized = 2 * Math.Sqrt((bins[i * 2] * bins[i * 2] + bins[i * 2 + 1] * bins[i * 2 + 1]) / 2048);
				var decibel = 20 * Math.Log10(normalized);

				if (decibel > max)
				{
					max = decibel;
				}

			}

			return max;
		}

		/// <summary>
		/// Finds the strongest bin above limit in given segment.
		/// </summary>
		/// <param name="bins">Complex values alternating Real and Imaginary values</param>
		/// <param name="from">lower bound</param>
		/// <param name="to">upper bound</param>
		/// <param name="limit">limit indicating weak bin</param>
		/// <param name="sensitivity">sensitivity of the limit (the higher the lower sensitivity)</param>
		/// <returns>index of strongest bin or null if none of the bins is strong enought</returns>
		public static int? GetStrongestBinIndex(double[] bins, int from, int to, double limit, double sensitivity = 0.9d)
		{
			var max = double.MinValue;
			int? index = null;
			for (int i = from; i < to; i++)
			{
				var normalized = 2 * Math.Sqrt((bins[i * 2] * bins[i * 2] + bins[i * 2 + 1] * bins[i * 2 + 1]) / 2048);
				var decibel = 20 * Math.Log10(normalized);

				if (decibel > max && decibel * sensitivity > limit)
				{
					max = decibel;
					index = i * 2;
				}

			}

			return index;
		}


		#region simple helpers

		/// <summary>
		/// Converts array of shorts to array of doubles
		/// </summary>
		/// <param name="audioData"></param>
		/// <returns></returns>
		public static double[] ShortArrayToDoubleArray(short[] audioData)
		{
			double[] res = new double[audioData.Length]; //allocate new memory
			for (int i = 0; i < audioData.Length; i++) //copy shorts to doubles
			{
				res[i] = audioData[i];
			}
			audioData = null; //free up memory
			return res;
		}

		/// <summary>
		/// Builds address from parts
		/// </summary>
		/// <param name="anchorFreq">Frequency of anchor point</param>
		/// <param name="pointFreq">Frequency of Self point</param>
		/// <param name="delta">Time delta between Anchor and Self point</param>
		/// <returns>Left to right: 9bits Anchor frequency, 9bits Self point frequency, 14 bits delta</returns>
		public static uint BuildAddress(in uint anchorFreq, in uint pointFreq, uint delta)
		{
			uint res = anchorFreq;
			res <<= 9; //move 9 bits 
			res += pointFreq;
			res <<= 14; //move 14 bits 
			res += delta;
			return res;
		}

		/// <summary>
		/// Builds song value out of parts
		/// </summary>
		/// <param name="absAnchorTime">Absolute time of anchor</param>
		/// <param name="id">Id of a song</param>
		/// <returns>Left to right: 32bits AbsAnchTime, 32 bits songID</returns>
		public static ulong BuildSongValue(in uint absAnchorTime, uint id)
		{
			ulong res = absAnchorTime;
			res <<= 32;
			res += id;
			return res;
		}

		#endregion
	}
}