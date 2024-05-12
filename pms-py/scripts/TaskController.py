import sys
import json
import csv

import env_settings

from Model import TaskModel

def TaskController():

  import Migrations.Task_migration as _migration
  import data.ApplicationDbContext as _context
  import Repository.RepositoryDatabase as _repository

  # if len(sys.argv) < 2:
  #   print("Недостаточно аргументов. Ожидается данные JSON в качестве аргумента.")
  #   sys.exit(1)

  # response data from c# MVC
  # data = json.loads(sys.argv[1])

  # data for csv
  data = [
    {
      "Id": 6,
      "Title": "Task1",
      "Description": "This is desc",
      "Beginning": "2020-01-01",
      "Ending": "2020-01-02",
      "AppUserIdSend": "50-2ogokkdng39-wkfk-sdoksokg0g34turi",
      "AppUserIdRecipient": "fjj9f033-okojwufe23f-f0w",
      "CompanyNumber": "C-40283259-PMS",
      "Status": "Completed"
    }
  ]

  # insert in csv file
  # csv_common.create(env_settings.csv_file_task, data)

  _repository.RepositoryDatabase.WriteInCsvTasks(data)

  # insert in db
  with open(env_settings.csv_file_task, newline='') as file:
    dataCSV = csv.reader(file, delimiter="|", quotechar="'")

    next(dataCSV)
    
    for row in dataCSV:
      context = _context.ApplicationDbContext(env_settings.connection_string,  _migration.INSERT_TABLE, 
                                        row, _migration.CREATE_TABLE)
      
      context.ApplicationDbContext()

TaskController()