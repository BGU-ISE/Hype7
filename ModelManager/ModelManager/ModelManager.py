
import sys
import NumericFeaturizer
import NumericModel
import DBConnection

global x 
x =  'C:/Users/alina/source/repos/Hype7U/ModelManager/ModelManager/tiktok_12_31_2021.csv'
global y 
y = 'C:/Users/alina/source/repos/Hype7U/ModelManager/ModelManager/tiktok_duplicate_1_7_2022.csv'
global db_file 
db_file = 'C:/Users/alina/source/repos/Hype7U/ModelManager/ModelManager/DataBase.db'

class ModelManager():
    def __init__(self):
        self.db = DBConnection.DBConnection(db_file)
        self.db.create_connection()
        self.df1 = self.db.get_numeric_dataframe(1)
        self.df7 = self.db.get_numeric_dataframe(7)  
    
    def test_train_and_fit(self):
        features = NumericFeaturizer.NumericFeaturizer(self.df1, self.df7)
        df = features.prepare_to_train()
        model_instance = NumericModel.Model(df)
        model_instance.split(0.2)
        model_instance.fit() 
        print(model_instance.predict(model_instance.X_test))   
        print("Accuracy: ",     model_instance.model.score(model_instance.X_test, model_instance.y_test))

    def test_predict_model_exists(self):
        features = NumericFeaturizer.NumericFeaturizer(self.df1)
        df = features.prepare_to_predict()
        model_instance = NumericModel.Model(df)
        print(model_instance.predict(df))   

    def main(self, num):    
        if num == "1" :
            self.test_train_and_fit()
        elif num == "2" :
            self.test_predict_model_exists()
        else :
            raise Exception("Command entered does not exist")


if __name__ == '__main__':
    command = sys.argv[1:]
    cmd = command[0]
    manager = ModelManager()
    manager.main(cmd)
    