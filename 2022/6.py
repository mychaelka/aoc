import sys
from collections import deque

def parse_input(input_file):
    with open(input_file) as input:
        txt = input.readlines()
    
    line = txt[0].strip('\n')
    return line


def find_packet_start(input, str_length):
    line = parse_input(input)
    current_idx = str_length - 1

    letters = deque([line[current_idx - i] for i in range(str_length)][::-1])

    while current_idx != len(line) - 1:
        if len(set(letters)) == str_length:
            return current_idx + 1
        else:
            current_idx += 1
            letters.popleft()
            letters.append(line[current_idx])
    
    return current_idx + 1

print(find_packet_start(sys.argv[1], 4))
print(find_packet_start(sys.argv[1], 14))