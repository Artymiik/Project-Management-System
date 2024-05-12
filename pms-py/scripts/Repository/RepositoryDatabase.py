import csv
import env_settings

class RepositoryDatabase:
  # Write in csv task
  def WriteInCsvTasks(data):
    
    if (data is None):
       return "Data is required"

    with open(env_settings.csv_file_task, 'w', newline='') as file:
      writer = csv.writer(file, delimiter='|', quotechar="'")

      writer.writerow(['Id', 'Title', 'Description', 'Beginning', 'Ending', 'AppUserIdSend', 'AppUserIdRecipient', 'CompanyNumber', 'Status', 'AppUserId'])

      for row in data:
          writer.writerow([row['Id'], row['Title'], row['Description'], row['Beginning'], row['Ending'], row['AppUserIdSend'], row['AppUserIdRecipient'], row['CompanyNumber'], row['Status'], None])