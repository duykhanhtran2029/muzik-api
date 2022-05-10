:ON ERROR EXIT
GO
:r $(FilePath)/DatabaseSchema.sql
:r $(FilePath)/DatabaseData.sql
GO