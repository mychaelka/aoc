def puzzle(calories):
    with open(calories, 'r') as input:
        lines = input.read()
        lines = lines.split('\n\n')
        newlines = [line.split('\n') for line in lines]
        newlines[-1].pop()

        intlines = []
        for line in newlines:
            intline = list(map(int, line))
            intlines.append(intline)
    
    max = [0,0,0]
    top_idx = 0

    for i in range(3):
        for idx, line in enumerate(intlines):
            current = sum(line)
            if current > max[i]:
                max[i] = current
                top_idx = idx
        intlines.pop(top_idx)

    print(max)
    
    return sum(max)


print(puzzle('input.txt'))
