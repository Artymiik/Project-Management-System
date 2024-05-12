import csv
import pyodbc

class ApplicationDbContext():

  def __init__(self, connection_string, response_insert, response_data, create_table):
      self.connection_string = connection_string
      self.response_insert = response_insert
      self.response_data = response_data
      self.create_table = create_table

  def ApplicationDbContext(self):
    
    with pyodbc.connect(self.connection_string) as connection:
       with connection.cursor() as cursor:
          #create table
          cursor.execute(self.create_table)

          #insert into table
          cursor.execute(self.response_insert, self.response_data)

    # close all
    cursor.close()
    connection.close()