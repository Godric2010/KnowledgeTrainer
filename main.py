import datetime
import os.path
from pathlib import Path
import curses
# import unicurses

data_folder = str(Path.home()) + "/KnowledgeTrainer"

settings = {}
cards = []

stdscr = None


def read_ini_file():
    if not os.path.exists(data_folder):
        os.mkdir(data_folder)

    if not os.path.exists(data_folder + "/start.ini"):
        ini_file = open(data_folder + "/start.ini", "w+")
        ini_file.write(
            "Username=\nInterval 1=1\nInterval 2=2\nInterval 3=3\nInterval 4=7\nInterval 5=14\nInterval 6=28\nDaily Limit=0")
        ini_file.close()

    ini_file = open(data_folder + "/start.ini", "r")
    if ini_file.mode == 'r':
        contents = ini_file.read().split('\n');
        for line in contents:
            key_value = line.split('=')
            settings[key_value[0]] = key_value[1]

    ini_file.close()


def write_ini_file():
    ini_file = open(data_folder + "/start.ini", "w+")
    content = "Username=" + settings['Username'] + "\nInterval 1=" + settings['Interval 1'] + "\nInterval 2=" + \
              settings['Interval 2'] + "\nInterval 3=" + settings['Interval 3'] + "\nInterval 4=" + settings[
                  'Interval 4'] + "\nInterval 5=" + settings['Interval 5'] + "\nInterval 6=" + settings[
                  'Interval 6'] + "\nDaily Limit=0"
    ini_file.write(content)
    ini_file.close()


def read_csv_database():
    if not os.path.exists(data_folder + "/db.csv"):
        database = open(data_folder + "/db.csv", "w+")
        database.write(";;;;;;")
        database.close()

    database = open(data_folder + "/db.csv", "r")
    if database.mode == 'r':
        contents = database.read().split('\n')
        for line in contents:
            card = {}
            elements = line.split(';')

            if elements[0] == '':
                continue

            card['ID'] = int(elements[0])
            card['Category'] = elements[1]
            card['Question'] = elements[2]
            card['Answer'] = elements[3]
            card['Level'] = int(elements[4])
            card['CreationDate'] = datetime.datetime.strptime(elements[5], "%m/%d/%Y")
            card['NextRepeat'] = datetime.datetime.strptime(elements[6], "%m/%d/%Y")

            cards.append(card)

    database.close()


def write_csv_database():
    database = open(data_folder + "/db.csv", "w+")
    content = ""
    for card in cards:
        content += str(card['ID']) + ";" + card['Category'] + ";" + card['Question'] + ";" + card['Answer'] + ";" + str(
            card['Level']) + ";" + datetime.date.strftime(card['CreationDate'],
                                                          "%m/%d/%Y") + ";" + datetime.date.strftime(card['NextRepeat'],
                                                                                                     "%m/%d/%Y") + "\n"

    database.write(content)
    database.close()
    database.close()


def input_text(stdscr, y_pos, x_pos) -> str:
    stdscr.move(y_pos, x_pos)
    text = ""
    while True:
        character = stdscr.getch()
        if character == 10:      #Enter key
            return text
        elif character == 127:  # Delete key
            text = text.rstrip(text[-1])
            stdscr.addstr(y_pos, x_pos, text + ' ')
            stdscr.move(y_pos, x_pos + len(text) - 1)
            continue

        text += chr(character)
        stdscr.addstr(y_pos, x_pos, text)


def create_new_card(category, question, answer):
    today = datetime.datetime.today()

    card = {'ID': len(cards) + 1, 'Category': category, 'Question': question, 'Answer': answer, 'Level': 0,
            'CreationDate': today, 'NextRepeat': today}

    cards.append(card)
    write_csv_database()


def get_next_interval_by_level(lvl) -> int:
    if lvl == 1:
        return int(settings['Interval 1'])
    elif lvl == 2:
        return int(settings['Interval 2'])
    elif lvl == 3:
        return int(settings['Interval 3'])
    elif lvl == 4:
        return int(settings['Interval 4'])
    elif lvl == 5:
        return int(settings['Interval 5'])
    elif lvl == 6:
        return int(settings['Interval 6'])
    else:
        return int(settings['Interval 6'])


def show_menu():
    stdscr = curses.initscr()
    curses.noecho()
    curses.cbreak()
    stdscr.keypad(True)

    stdscr.clear()
    title = "Knowledge Trainer 1.0"
    subtitle = "by Sebastian Borsch"
    screen_size = stdscr.getmaxyx()
    stdscr.addstr(0, int(screen_size[1] / 2 - (len(title) / 2)), title, curses.A_BOLD)
    stdscr.addstr(1, int(screen_size[1] / 2 - (len(subtitle) / 2)), subtitle, curses.A_DIM)

    if settings['Username'] == '':
        enter_username_dialog(stdscr, screen_size)

    show_welcome_screen(stdscr, screen_size)

    stdscr.clear()

    curses.nocbreak()
    stdscr.keypad(False)
    curses.echo()

    curses.endwin()


def enter_username_dialog(stdscr, screen_size):
    yStartPos = int(screen_size[0] / 2 - 3)
    xStartPos = int(screen_size[1] / 5)
    stdscr.addstr(yStartPos, xStartPos, "Du musst einer neuer Nutzer sein. Wie heisst du?")
    stdscr.addstr(yStartPos + 1, xStartPos, "(Namen eingeben und mit ENTER bestätigen)", curses.A_LOW)

    settings['Username'] = input_text(stdscr, yStartPos + 6, xStartPos)
    write_ini_file()
    stdscr.clear()


def show_welcome_screen(stdscr, screen_size):
    y_start_pos = int(screen_size[0] / 2 - 4)
    x_start_pos = int(screen_size[1] / 5)

    while True:
        stdscr.addstr(y_start_pos, x_start_pos, "Willkommen beim Knowledge Trainer, " + settings['Username'],
                      curses.A_BOLD)
        stdscr.addstr(y_start_pos + 1, x_start_pos, "Wähle eine Nummer um fortzufahren.")
        stdscr.addstr(y_start_pos + 3, x_start_pos, "[1] Abfrage starten.")
        stdscr.addstr(y_start_pos + 4, x_start_pos, "[2] Neue Fragen anlegen.")
        stdscr.addstr(y_start_pos + 5, x_start_pos, "[3] Einstellungen bearbeiten.")
        stdscr.addstr(y_start_pos + 6, x_start_pos, "[4] Verlassen.")

        selection = chr(stdscr.getch())
        stdscr.clear()

        if int(selection) == 1:
            ask_questions(stdscr, screen_size)
            break
        elif int(selection) == 2:
            create_new_questions(stdscr, screen_size)
        elif int(selection) == 3:
            pass
        elif int(selection) == 4:
            return
        else:

            stdscr.addstr(y_start_pos, x_start_pos, "Ungültige Eingabe...", curses.A_BOLD)
            stdscr.refresh()
            curses.napms(2000)
            stdscr.clear()


def ask_questions(stdscr, screen_size):
    y_start_pos = int(screen_size[0] / 2 - 4)
    x_start_pos = int(screen_size[1] / 5)

    questions = []
    today = datetime.date.today()
    for card in cards:
        next_repeat = card['NextRepeat'].date()
        if next_repeat <= today:
            questions.append(card)

    index = 0
    while index < len(questions):
        stdscr.addstr(y_start_pos, x_start_pos,
                      "Kategorie: " + questions[index]['Category'] + " [Frage " + str(index + 1) + " von " + str(
                          len(questions)) + "]", curses.A_BOLD)
        stdscr.addstr(y_start_pos + 1, x_start_pos, questions[index]['Question'])
        stdscr.addstr(y_start_pos + 3, x_start_pos, "Deine Antwort: ")
        input_text(stdscr, y_start_pos + 3, x_start_pos + len("deine antwort: "))
        stdscr.addstr(y_start_pos + 5, x_start_pos, "Antwort: " + questions[index]["Answer"])
        stdscr.addstr(y_start_pos + 7, x_start_pos, "Ist deine Antwort korrekt? (J/N)")
        while True:
            char = input_text(stdscr, y_start_pos + 7, x_start_pos + 2 + len("Ist deine Antwort korrekt? (J/N)"))
            if char == "j":
                questions[index]['Level'] += 1
                questions[index]['NextRepeat'] += \
                    datetime.timedelta(days=get_next_interval_by_level(questions[index]['Level']))
                break
            elif char == "n":
                questions[index]['Level'] = 1
                questions[index]['NextRepeat'] = datetime.datetime.today()
                questions.append(questions[index])
                break

        stdscr.addstr(y_start_pos + 9, x_start_pos, "Neues Level: " +
                      str(questions[index]['Level']) + " Nächste Abfrage: " + datetime.datetime.strftime(
            questions[index]['NextRepeat'], "%d/%m/%Y"))

        index += 1
        stdscr.refresh()
        curses.napms(3000)
        stdscr.clear()

    write_csv_database()
    stdscr.addstr(y_start_pos, x_start_pos, "Alle Fragen für heute geschafft! Gut gemacht!")
    stdscr.refresh()
    curses.napms(3000)
    stdscr.clear()


def create_new_questions(stdscr, screen_size):
    y_start_pos = int(screen_size[0] / 2 - 4)
    x_start_pos = int(screen_size[1] / 5)

    category_text = "Kategorie: "
    confirmation_question = "Frage hinzufuegen? (J/N/ Q zum beenden / C zum beenden ohne speichern) "

    while True:
        stdscr.addstr(y_start_pos, x_start_pos, "Neue Frage anlegen:", curses.A_BOLD)
        stdscr.addstr(y_start_pos + 2, x_start_pos, category_text)
        stdscr.addstr(y_start_pos + 3, x_start_pos, "Frage: ")
        stdscr.addstr(y_start_pos + 4, x_start_pos, "Antwort: ")

        category = input_text(stdscr, y_start_pos + 2, x_start_pos + len(category_text))
        question = input_text(stdscr, y_start_pos + 3, x_start_pos + len(category_text))
        answer = input_text(stdscr, y_start_pos + 4, x_start_pos + len(category_text))

        stdscr.addstr(y_start_pos + 6, x_start_pos, confirmation_question)
        x = x_start_pos + 2 + len(confirmation_question)
        while True:
            char = input_text(stdscr, y_start_pos + 6, x)
            if char == "j":
                create_new_card(category, question, answer)
                stdscr.addstr(y_start_pos + 8, x_start_pos, "Neue Frage erstellt.")
                break
            elif char == "n":
                stdscr.addstr(y_start_pos + 8, x_start_pos, "Frage verworfen")
                break
            elif char == "q":
                create_new_card(category, question, answer)
                stdscr.clear()
                return
            elif char == "c":
                stdscr.clear()
                return
            stdscr.addstr(y_start_pos + 4, x_start_pos, " " * len(char))

        stdscr.refresh()
        curses.napms(2000)
        stdscr.clear()


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    read_ini_file()
    read_csv_database()
    show_menu()
    # wrapper(show_menu)

# See PyCharm help at https://www.jetbrains.com/help/pycharm/
