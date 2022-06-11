
import NumericFeaturizer
import NumericModel
import DBConnection
import YouTubeFeaturizer
import YouTubeModel
import os
import csv
import pandas as pd
import numpy as np # linear algebra
from pathlib import Path

from sklearn.metrics import mean_squared_error, mean_absolute_error

class ModelManager():
    global x 
    x =  '\\tiktok_12_31_2021.csv'
    global y 
    y = '\\tiktok_duplicate_1_7_2022.csv'
    global db_file 
    db_file = '\\DataBase.db'
    global db_directory
    db_directory = Path(__file__).parent.parent.parent
    global project_db_path
    project_db_path = db_directory /  "Metric Manager/Metric Manager/bin/Debug/net5.0/DataBase.db"
    global dir 
    dir = os.path.dirname(__file__)

    def __init__(self, fromDB): 
        self.is_DB = fromDB
        filename = str(project_db_path) #dir + db_file
        self.db = DBConnection.DBConnection(filename)
        self.db.create_connection()
        if(fromDB):
            self.df1 = self.db.get_numeric_Youtube_dataframe(1)
            self.df7 = self.db.get_numeric_Youtube_dataframe(7)
        else:
            self.df1 = pd.read_csv(dir + x) 
            self.df7 = pd.read_csv(dir + y)

    def train_and_fit_Youtube(self):
        features = YouTubeFeaturizer.YouTubeFeaturizer(self.is_DB, self.df1, self.df7)
        df = features.prepare_to_train()
        
        model_instance = YouTubeModel.Model(df)
        model_instance.split(0.2)

        model_instance.fit() 
        predictions = model_instance.predict(model_instance.X_test_id)
        self.print_regression_analysis(model_instance.y_test, predictions)
        self.db.write_predictions_to_DB(predictions, model_instance.X_test_id['video_id'].tolist())
    
    def predict_Youtube_model_exists(self):
        features = YouTubeFeaturizer.YouTubeFeaturizer(self.is_DB, self.df1)
        df = features.prepare_to_predict()
        model_instance = YouTubeModel.Model(df)
        print(model_instance.predict(df)) 
        self.db.write_predictions_to_DB(predictions, model_instance.X_test_id['video_id'].tolist())


    def predictions_to_csv(self, predictions, model_instance):
        denorm = features.denormalize(predictions)
        dv = model_instance.get_dv()
        d= dv['dv_check'].tolist()
        self.print_regression_analysis(dv, denorm)
        df = pd.DataFrame({'video_id': model_instance.X_test_id['video_id'].tolist(), 'model1score' : predictions, 'normal':denorm, 'dv':d})
        df.to_csv('predictions.csv')
       

    def train_and_fit(self):
        features = NumericFeaturizer.NumericFeaturizer( self.is_DB, self.df1, self.df7)
        df = features.prepare_to_train()
        model_instance = NumericModel.Model(df)
        model_instance.split(0.2)
        model_instance.fit() 
        predictions = model_instance.predict(model_instance.X_test_id)
        self.print_regression_analysis(model_instance.y_test, predictions)

        self.db.write_predictions_to_DB(predictions, model_instance.X_test_id['id'].tolist())

    def print_regression_analysis(self, y_actual, y_prediction):
        mae = mean_absolute_error(y_actual, y_prediction)
        mse = mean_squared_error(y_actual, y_prediction)
        rmse = np.sqrt(mse)
        print("RMSE: %f" % (rmse), "MSE: %f" % (mse), "MAE: %f" % (mae))

    def predict_model_exists(self):
        features = NumericFeaturizer.NumericFeaturizer(self.is_DB, self.df1)
        df = features.prepare_to_predict()
        model_instance = NumericModel.Model(df)
        print(model_instance.predict(df))  

    def export_df_to_csv(df, filename):
        df.to_csv(filename)


if __name__ == '__main__':
    model = ModelManager(True)
    model.train_and_fit_Youtube()
