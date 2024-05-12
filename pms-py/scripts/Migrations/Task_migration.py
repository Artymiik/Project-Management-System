# Create table AppTask
CREATE_TABLE = '''create table if not exists AppTask(Id INT PRIMARY KEY, Title NVARCHAR(255), 
                              Description NVARCHAR(MAX), Beginning DATETIME, Ending DATETIME, AppUserIdSend Text, 
                                      AppUserIdSendRecipient Text, CompanyNumber Text, Status Text, AppUserId)'''

INSERT_TABLE = '''insert into AppTask(Id, Title, Description, Beginning, Ending, AppUserIdSend, 
                        AppUserIdSendRecipient, CompanyNumber, Status, AppUserId) 
                                        values(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)'''