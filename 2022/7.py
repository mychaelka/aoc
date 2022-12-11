import sys

class Directory:
    def __init__(self, name, parent):
        self.name = name
        self.size = 0
        self.parent = parent
        self.subdirs = dict()
        self.files = dict()


class DirTree:
    def __init__(self, root):
        self.root = root


def parse_input(input):
    with open(input) as input_file:
        txt = input_file.readlines()
    
    cmdline = []
    for line in txt:
        cmdline.append(line.strip('\n'))
    
    return cmdline


def build_dirtree(cmdline):
    root = Directory('/', None)
    current_dir = root
    for line in cmdline:
        output = line.split()
        if line[0] == '$':
            current_dir = parse_command(output, current_dir)
        else:
            parse_cmd_output(output, current_dir)
    return root


def parse_cmd_output(line, current_dir):
    if line[0] == 'dir':
        name = line[1]
        if name not in current_dir.subdirs:
            current_dir.subdirs[name] = Directory(name, current_dir)
    else:
        size = line[0]
        name = line[1]
        if name not in current_dir.files:
            current_dir.files[name] = size
        current_dir.size += int(size)
        return_to_root(current_dir, int(size))


def parse_command(line, current_dir):
    if line[1] == 'cd':
        if line[2] == '..':
            current_dir = current_dir.parent
        elif line[2] == '/':
            current_dir = return_to_root(current_dir, 0)
        else:
            current_dir = current_dir.subdirs[line[2]]
        return current_dir 
    else:
        return current_dir
    

def return_to_root(current_dir, size):
    dir = current_dir
    while dir.parent is not None:
        dir = dir.parent
        dir.size += size
    return dir


def print_dirs(root, offset):
    print(offset * " " + "-", root.name)
    for name, dir in root.subdirs.items():
        print_dirs(dir, offset + 2)
    for filename, size in root.files.items():
        print(offset * " " + "*", filename, size)

def dirs_sizes(dir, size):
    if dir.size <= 100000:
        size[0] += dir.size
    for name, subdir in dir.subdirs.items():
        dirs_sizes(subdir, size)  


def min_free_space(dir, size, necessary):
    if dir.size < size and dir.size >= necessary:
        size = dir.size
    for name, subdir in dir.subdirs.items():
        size = min(size, min_free_space(subdir, size, necessary))
    return size


def solution():
    # part 1
    cmdline = parse_input(sys.argv[1])
    root = build_dirtree(cmdline)
    size = [0]
    dirs_sizes(root, size)
    print(size[0])

    # part 2
    totalspace = 70000000
    freespace = totalspace - root.size
    necessary = 30000000 - freespace
    min_dir_size = root.size
    min_size = min_free_space(root, min_dir_size, necessary)
    print(min_size)


solution()

