from sklearn.model_selection import cross_val_score
from sklearn.model_selection import RepeatedKFold
from sklearn.model_selection import train_test_split

from numpy import absolute
from IModel import *
import xgboost as xgb  #model 
import pandas as pd


class Model(IModel):
  def __init__(self, datafile):
    #self.df = pd.read_csv(datafile)
    self.df = datafile
    self.model = xgb.XGBRegressor()
    

  def split(self, test_size):
    columns = self.df.columns.values.tolist()
    columns.remove('dv_view_count')
    y = self.df[["dv_view_count"]]
    X = self.df[columns]
    self.X_train_id, self.X_test_id, self.y_train, self.y_test = train_test_split(X, y, test_size=0.2, random_state=123)
    self.X_train = self.X_train_id.drop(labels='video_id', axis=1)
    self.X_test = self.X_test_id.drop(labels='video_id', axis=1)

  def fit(self):
    # define model evaluation method
    self.cv = RepeatedKFold(n_splits=10, n_repeats=3, random_state=1)
    # evaluate model
    self.scores = cross_val_score(self.model, self.X_train, self.y_train, scoring='neg_mean_absolute_error', cv=self.cv, n_jobs=-1)
    # force scores to be positive
    self.scores = absolute(self.scores)
    print('Train: Mean: %.3f STD: (%.3f)' % (self.scores.mean(), self.scores.std()) )
    self.model = self.model.fit(self.X_train, self.y_train)
    self.model.save_model("model.txt")

  def predict(self, data=None):
    model2 = xgb.XGBRegressor()
    model2.load_model("model.txt")
    data_without_id = data.drop(labels='video_id', axis=1)
    result = model2.predict(data_without_id)
    compare = pd.DataFrame({'id': data['video_id'].tolist(), 'Prediction' : result})
    return result



