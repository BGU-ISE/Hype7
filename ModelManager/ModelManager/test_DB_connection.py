import unittest
import NumericFeaturizer
import NumericModel
import DBConnection
class Test_test_DB_connection(unittest.TestCase):
    global db_file 
    db_file = 'C:/Users/alina/source/repos/Hype7U/ModelManager/ModelManager/DataBase.db'
    def test_A(self):
        self.db = DBConnection.DBConnection(db_file)
        self.db.create_connection()
        df1 = self.db.get_numeric_dataframe(1)
        print(df1)

if __name__ == '__main__':
    unittest.main()
