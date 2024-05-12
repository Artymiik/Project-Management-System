class TaskModel:
  def __init__(self, Id: int, Title: str, Description: str, Beginning: str, 
               Ending: str, AppUserIdSend: str, AppUserIdSendRecipient: str, 
               Company: str, Status: str):
    
    self.Id = Id
    self.Title = Title
    self.Description = Description
    self.Beginning = Beginning
    self.Ending = Ending
    self.AppUserIdSend = AppUserIdSend
    self.AppUserIdSendRecipient = AppUserIdSendRecipient
    self.Company = Company
    self.Status = Status