import sys

def main(num):
    if num == 1 :
        print("hi 1")
    else :
        print ("hi ", num)


if __name__ == '__main__':
    args = sys.argv[1:]
    main(args)
    
