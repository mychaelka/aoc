import sys

def parse_input(input):
    with open(input) as input_file:
        pairs = input_file.readlines()
    
    intervals = []
    for item in pairs:
        intervals.append(item.strip('\n').split(','))
    
    return intervals


def create_num_interval(intervals):
    interval1 = [int(num) for num in intervals[0].split('-')]
    interval2 = [int(num) for num in intervals[1].split('-')]

    num_interval = [interval1, interval2]
    return num_interval


def find_full_overlaps(intervals_list):
    count = 0
    for intervals in intervals_list:
        num_intervals = create_num_interval(intervals)
        int1 = num_intervals[0]
        int2 = num_intervals[1]
        if (int1[0] >= int2[0] and int1[1] <= int2[1]) or \
            (int2[0] >= int1[0] and int2[1] <= int1[1]):
            count += 1
    
    return count


def find_overlaps(intervals_list):
    count = 0
    for intervals in intervals_list:
        num_intervals = create_num_interval(intervals)
        int1 = num_intervals[0]
        int2 = num_intervals[1]
        if not (int1[1] < int2[0] or int2[1] < int1[0]):
            count += 1
    
    return count


def task1():
    intervals_list = parse_input(sys.argv[1])
    overlaps = find_full_overlaps(intervals_list)
    print(overlaps)


def task2():
    intervals_list = parse_input(sys.argv[1])
    overlaps = find_overlaps(intervals_list)
    print(overlaps)


task1()
task2()