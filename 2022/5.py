import sys

def parse_input(input_file):
    with open(input_file) as input:
        text = input.readlines()
    
    rows = []
    for i in range(8):
        rows.append(text[i])
    
    stacks = parse_crates(rows)
    instructions = text[10:]
    return stacks, instructions


def parse_crates(crates_input):
    stacks = [[] for i in range(9)]
    for row in crates_input[::-1]:
        crates = row.replace('    ', '[-]').replace('][', '] [').strip('\n').split(' ')
        for crate_idx, crate in enumerate(crates):
            if crate != '[-]':
                stacks[crate_idx].append(crate)
    return stacks


def parse_instruction(instruction):
    nums = [int(i) for i in instruction.split() if i.isdigit()]
    return nums[0], nums[1], nums[2]


def move_boxes(instructions, stacks):
    for instruction in instructions:
        amount, from_idx, to_idx = parse_instruction(instruction)
        for i in range(amount):
            stacks[to_idx - 1].append(stacks[from_idx - 1].pop())


def move_boxes_new(instructions, stacks):
    for instruction in instructions:
        amount, from_idx, to_idx = parse_instruction(instruction)
        stacks[to_idx - 1].extend(stacks[from_idx - 1][-amount:])
        del stacks[from_idx - 1][-amount:]


def get_top_boxes(stacks):
    return [stack[-1] for stack in stacks]


def task1(input):
    stacks, instructions = parse_input(input)
    move_boxes(instructions, stacks)
    tops = get_top_boxes(stacks)
    print(tops)


def task2(input):
    stacks, instructions = parse_input(input)
    move_boxes_new(instructions, stacks)
    tops = get_top_boxes(stacks)
    print(tops)


task1(sys.argv[1])
task2(sys.argv[1])