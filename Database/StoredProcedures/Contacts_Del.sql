CREATE PROCEDURE [dbo].[Contacts_Del]
	@contactId int
AS
	delete Contacts where Id = @contactId
RETURN 0