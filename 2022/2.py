with open("input2.txt") as input:
    data = input.readlines()

rounds = []

for line in data:
    rounds.append(line.strip('\n').split(' '))

print(rounds)

scores = {'X': 1, 'Y': 2, 'Z': 3}

# X - lose 
# Y - draw
# Z - win

score = 0

for round in rounds:
    opponent = round[0]
    me = round[1]    
    score += scores[round[1]]
    if opponent == 'A':  # rock
        if me == 'X':
            score += 3
        if me == 'Y':
            score += 6

    if opponent == 'B':  # paper 
        if me == 'Y':
            score += 3
        if me == 'Z':
            score += 6

    if opponent == 'C':  # scissors
        if me == 'X':
            score += 6
        if me == 'Z':
            score += 3

print(score)

lose = {'A': 'Z', 'B': 'X', 'C': 'Y'}
win = {'A': 'Y', 'B': 'Z', 'C': 'X'}
draw = {'A': 'X', 'B': 'Y', 'C': 'Z'}

newscore = 0
for round in rounds:
    opponent = round[0]
    result = round[1]
    if result == 'X':  # lose
        newscore += scores[lose[opponent]]
    if result == 'Y':
        newscore += scores[draw[opponent]]
        newscore += 3
    if result == 'Z':
        newscore += scores[win[opponent]]
        newscore += 6

print(newscore)