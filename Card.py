from enum import Enum
import datetime


class CardProperty(Enum):
    ID = 0
    Category = 1
    Question = 2
    Answer = 3
    Level = 4
    CreationDate = 5
    NextRepeat = 6


class QuestionCard:

    def __init__(self, data):
        self.ID = int(data[0])
        self.Category = str(data[1])
        self.Question = str(data[2])
        self.Answer = str(data[3])
        self.Level = int(data[4])
        self.CreationData = datetime.datetime.strptime(data[5], "%m/%d/%Y")
        self.NextRepeat = datetime.datetime.strptime(data[6], "%m/%d/%Y")
