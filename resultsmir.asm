data segment para public "data"
a  dl    1
s  dl    1
r  dl    1
PRINT_BUF DB ' ' DUP(10)
BUFEND    DB '$'
data ends
stk segment stack
db 256 dup ("?")
stk ends
code segment para public "code"
main proc
assume cs:code,ds:data,ss:stk
mov ax,data
mov ds,ax
mov ax,5
push ax
pop ax
mov s, ax
mov ax,9
push ax
pop ax
mov r, ax
mov ax,s
push ax
mov ax,r
push ax
pop bx
pop ax
add ax,bx
push ax
pop ax
mov a, ax
mov ax,a
push ax
mov ax,a
push ax
mov ax,s
push ax
pop bx
pop ax
mul bx
push ax
pop bx
pop ax
add ax,bx
push ax
pop ax
mov r, ax
label1:
mov ax,s
push ax
mov ax,8
push ax
pop ax
pop bx
cmp bx, ax
jge label2
mov ax,s
push ax
mov ax,1
push ax
pop bx
pop ax
add ax,bx
push ax
pop ax
mov s, ax
jmp label1
label2:
push ax
mov ax, r
CALL PRINT
pop ax
mov ax,4c00h
int 21h
main endp
PRINT PROC NEAR
MOV   CX, 10
MOV   DI, BUFEND - PRINT_BUF
PRINT_LOOP:
MOV   DX, 0
DIV   CX
ADD   DL, '0'
MOV   [PRINT_BUF + DI - 1], DL
DEC   DI
CMP   AL, 0
JNE   PRINT_LOOP
LEA   DX, PRINT_BUF
ADD   DX, DI
MOV   AH, 09H
INT   21H
RET
PRINT ENDP
code ends
end main