# LittleBASIC
Tiny BASIC implementation

LittleBASIC is loosely based on the original Dartmouth BASIC of the 1960s. The syntax is almost exactly the same.

HELLOWORLD.lb:
```
10 PRINT "HELLO, WORLD!"
20 GOTO 10
```

LittleBASIC counter program:
```
10 PRINT "ENTER A NUMBER LESS THAN 10:"
20 INPUT N
30 N = INT(N)
40 WHILE N < 11 DO
50 PRINT N
60 N = N + 1
70 WEND
80 END
```
