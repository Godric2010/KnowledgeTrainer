class SelectionMenu:

    def __init__(self, title, instruction):
        self.items = []
        self.title = title
        self.instruction = instruction

    def add_item(self, item_name, action):
        self.items.append((item_name, action))

    def get_item_names(self) -> [str]:
        name_list = []
        for item in range(len(self.items)):
            index_str = "[" + str(item + 1) + "] "
            name_list.append(index_str + self.items[item][0])

        return name_list
