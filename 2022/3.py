import sys

def parse_input(input):
    with open(input) as input_file:
        items = input_file.readlines()
    return items


def find_common(item):
    num_items = len(item)
    compartment1 = set(item[:num_items//2])
    compartment2 = set(item[num_items//2:])
    common = compartment1.intersection(compartment2)
    
    return list(common)[0]


def find_common_threesomes(items):
    clean_items = [item.replace("\n", "") for item in items]
    sets = list(map(set, clean_items))
    common = set.intersection(*sets)
    return list(common)[0]


def priority(letter):
    if ord(letter) >= 97:
        return(ord(letter) - 96)
    return(ord(letter) - 38)


def sum_priorities(input):
    items = parse_input(input)
    result = 0

    for item in items:
        common = find_common(item)
        result += priority(common)
    
    return result


def sum_priorities_threesomes(input):
    items = parse_input(input)
    result = 0

    for i in range(0, len(items), 3):
        common = find_common_threesomes(items[i:i+3])
        result += priority(common)
    
    return result


task1 = sum_priorities(sys.argv[1])
task2 = sum_priorities_threesomes(sys.argv[1])

print(task1, task2)