import NumericFeaturizer
import NumericModel
class class1(object):
    """description of class"""



if __name__ == '__main__':

    x = 'C:/Users/alina/source/repos/Hype7/ModelManager/ModelManager/tiktok_12_31_2021.csv'
    y = 'C:/Users/alina/source/repos/Hype7/ModelManager/ModelManager/tiktok_duplicate_1_7_2022.csv'
    features = NumericFeaturizer.NumericFeaturizer(x,y)
    df = features.prepare_to_train()
    model_instance = NumericModel.Model(df)
    model_instance.split(0.2)
    model_instance.fit() 
    #print(model_instance.predict())   
    print("Accuracy: ",     model_instance.model.score(model_instance.X_test, model_instance.y_test))