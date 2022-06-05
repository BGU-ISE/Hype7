import numpy as np # linear algebra
import pandas as pd # data processing, CSV file I/O (e.g. pd.read_csv)
from tqdm import tqdm
from xgboost.rabit import is_distributed #For memory function   reduce_mem_usage()

class NumericFeaturizer():
    global numeric_columns 
    numeric_columns =  ['id', 'createTime', 'authorMeta.id', 'authorMeta.verified','authorMeta.following', 'authorMeta.fans', 'authorMeta.heart', 'authorMeta.video', 'authorMeta.digg', 'musicMeta.musicId','musicMeta.musicOriginal', 'diggCount', 'shareCount', 'playCount', 'commentCount']

    def __init__(self, is_DB, datafile, labelfile=None):  
        try: 
           self.df = datafile
        except:
            print('Unable to readcsv of dataframe')
        else: 
            self.labels  = labelfile
            if(not is_DB):
                self.df = datafile[numeric_columns]
            self.continuous_train = self.df.columns.values.tolist()
            self.continuous_train.append('dv_playCount')
            self.continuous_train.remove('id')
            self.continuous_predict = self.df.columns.values.tolist() 

            self.df = self.reduce_mem_usage(self.df)


    def reduce_mem_usage(seldf, df):
        start_mem = df.memory_usage().sum() / 1024 ** 2
        print('Memory usage of Dataframe is {:.3f} MB'.format(start_mem))

        for col in tqdm(df.columns):
            col_type = df[col].dtype

            if col_type != object and col_type.name != 'category' and 'datetime' not in col_type.name:
                c_min = df[col].min()
                c_max = df[col].max()
                if str(col_type)[:3] == 'int':
                    if c_min > np.iinfo(np.int8).min and c_max < np.iinfo(np.int8).max:
                        df[col] = df[col].astype(np.int8)
                    elif c_min > np.iinfo(np.int16).min and c_max < np.iinfo(np.int16).max:
                        df[col] = df[col].astype(np.int16)
                    elif c_min > np.iinfo(np.int32).min and c_max < np.iinfo(np.int32).max:
                        df[col] = df[col].astype(np.int32)
                    elif c_min > np.iinfo(np.int64).min and c_max < np.iinfo(np.int64).max:
                        df[col] = df[col].astype(np.int64)

                else:
                    if c_min > np.finfo(np.float16).min and c_max < np.finfo(np.float16).max:
                        df[col] = df[col].astype(np.float16)
                    elif c_min > np.finfo(np.float32).min and c_max < np.finfo(np.float32).max:
                        df[col] = df[col].astype(np.float32)
                    else:
                        df[col] = df[col].astype(np.float64)

        end_mem = df.memory_usage().sum() / 1024 ** 2
        print('Memory usage after optimization is: {:.3f} MB'.format(end_mem))
        print('Decreased by {:.1f}%'.format(100 * (start_mem - end_mem) / start_mem))

        return df


    def normalize(self, continuous):
        for feature_name in continuous:
            max_value = self.df[feature_name].max()
            min_value = self.df[feature_name].min()
            self.df[feature_name] = (self.df[feature_name] - min_value) / (max_value - min_value)
        
    def prepare_to_train(self):
       try: 
           if(self.labels is None):
               raise('')
       except:
            print('No label file is given Unable to train')
       else: 
           self.labels = self.reduce_mem_usage(self.labels)
           self.labels = self.labels[['id', 'playCount']]
           self.df = self.df.merge(self.labels, on='id', how='inner')
           self.df["dv_playCount"]=round( self.df["playCount_y"]- self.df["playCount_x"] /7,4)
           self.df = self.df.drop(['playCount_y'], axis=1)
           self.df  = self.df.rename(columns={"playCount_x": "playCount"})

           self.normalize(self.continuous_train)
           return self.df

    def prepare_to_predict(self):
       self.normalize(self.continuous_predict)
       return self.df
