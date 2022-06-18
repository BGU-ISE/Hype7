
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
import sys

from sklearn.metrics import mean_squared_error, mean_absolute_error

class ModelManager():
   

    def __init__(self, fromDB, data_path=None): 
        self.is_DB = fromDB
        if(data_path == None):
            filename = str(project_db_path) #dir + db_file
        else:
            filename = data_path
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
        predictions, data_frame = model_instance.predict(model_instance.X_test_id)
        self.print_regression_analysis(model_instance.y_test, predictions)
        denorm = features.denormalize(predictions)
        dv = model_instance.get_dv()
        d= dv['dv_check'].tolist()
        self.print_regression_analysis(dv, denorm)

        self.db.write_predictions_to_DB('YoutubeModel',predictions, model_instance.X_test_id['video_id'].tolist(), denorm)
    
    def predict_Youtube_model_exists(self):
        features = YouTubeFeaturizer.YouTubeFeaturizer(self.is_DB, self.df1)
        df = features.prepare_to_predict()
        model_instance = YouTubeModel.Model(df)
        predictions, data_frame = model_instance.predict(df)
        print(predictions) 
        denorm = features.denorm_to_hundred(predictions)
        self.db.write_predictions_to_DB_without_train('ModelHypeScore', data_frame)


    def predictions_to_csv(self, predictions, model_instance):
        denorm = model_instance.denormalize(predictions)
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
        denorm = features.denormalize(predictions)
        dv = model_instance.get_dv()
        d= dv['dv_check'].tolist()
        self.print_regression_analysis(dv, denorm)
        self.db.write_predictions_to_DB('TiktokModel', predictions, model_instance.X_test_id['id'].tolist(), denorm)

    def train_and_fit1(self):
        features = NumericFeaturizer.NumericFeaturizer( self.is_DB, self.df1, self.df7)
        df = features.prepare_to_train()
        model_instance = NumericModel.Model(df)
        model_instance.split(0.2)
        model_instance.fit() 
        predictions = model_instance.predict(model_instance.X_test_id)
        self.print_regression_analysis(model_instance.y_test, predictions)

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
    #model = ModelManager(True)
   #model.predict_Youtube_model_exists()
    
  
    if(not len(sys.argv)==1):
        model = ModelManager(True, sys.argv[1])
        model.predict_Youtube_model_exists()
    else:
        model = ModelManager(True)
        model.predict_Youtube_model_exists()
    