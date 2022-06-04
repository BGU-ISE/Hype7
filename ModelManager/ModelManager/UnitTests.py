import unittest
import NumericFeaturizer
import NumericModel
import DBConnection
import os

class Test_test_1(unittest.TestCase):
    global x 
    x =  '\\tiktok_12_31_2021.csv'
    global y 
    y = '\\tiktok_duplicate_1_7_2022.csv'
    global db_file 
    db_file = '\\DataBase.db'
    global dir 
    dir = os.path.dirname(__file__)

    def setUp(self):
        filename = dir + db_file
        self.db = DBConnection.DBConnection(filename)
        self.db.create_connection()
        self.df1 = self.db.get_numeric_dataframe(1)
        self.df7 = self.db.get_numeric_dataframe(7)


    def test_db_connection(self):
        df = self.db.get_numeric_dataframe(1)

    def test_db_(self):
        features = NumericFeaturizer.NumericFeaturizer(True, self.df1, self.df7)
        df = features.prepare_to_train()
        model_instance = NumericModel.Model(df)
        model_instance.split(0.2)
        model_instance.fit() 
        print(model_instance.predict(model_instance.X_test))   
        print("Accuracy: ",     model_instance.model.score(model_instance.X_test, model_instance.y_test))


    def test_train_and_fit(self):
        features = NumericFeaturizer.NumericFeaturizer(True, self.df1, self.df7)
        df = features.prepare_to_train()
        model_instance = NumericModel.Model(df)
        model_instance.split(0.2)
        model_instance.fit() 
        print(model_instance.predict(model_instance.X_test))   
        print("Accuracy: ",     model_instance.model.score(model_instance.X_test, model_instance.y_test))

    def test_predict_model_exists(self):
        features = NumericFeaturizer.NumericFeaturizer
        df = features.prepare_to_predict()
        model_instance = NumericModel.Model(df)
        print(model_instance.predict(df))   

    def test_train_without_labels(self):
        features = NumericFeaturizer.NumericFeaturizer(True, self.df1)
        features.prepare_to_train()
        self.assertRaises(Exception)
    
    def test_preprocess_without_data(self):
        features = NumericFeaturizer.NumericFeaturizer(True, None)
        self.assertRaises(Exception)


if __name__ == '__main__':
    unittest.main()
