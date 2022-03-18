from abc import ABCMeta, abstractmethod

class IModel:
    __metaclass__ = ABCMeta

    @classmethod
    def version(self): return "1.0"
    
    @abstractmethod
    def split(self, test_size): raise NotImplementedError
        
    @abstractmethod    
    def fit(self): raise NotImplementedError
       
    @abstractmethod
    def predict(self): raise NotImplementedError





