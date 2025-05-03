.data
newline: .asciz "\n"
heap: .space 4096

.text
.global _start

_start:
    adr x10, heap         // Inicio del heap
    mov x9, x10           // Guardar inicio en x9 para imprimir luego

    // Construir "Hola, mundo!\0" (sin '¡' para evitar UTF-8 error)
    mov w0, #'H'
    strb w0, [x10], #1
    mov w0, #'o'
    strb w0, [x10], #1
    mov w0, #'l'
    strb w0, [x10], #1
    mov w0, #'a'
    strb w0, [x10], #1
    mov w0, #','
    strb w0, [x10], #1
    mov w0, #' '
    strb w0, [x10], #1
    mov w0, #'m'
    strb w0, [x10], #1
    mov w0, #'u'
    strb w0, [x10], #1
    mov w0, #'n'
    strb w0, [x10], #1
    mov w0, #'d'
    strb w0, [x10], #1
    mov w0, #'o'
    strb w0, [x10], #1
    mov w0, #'!'
    strb w0, [x10], #1
    mov w0, #0
    strb w0, [x10]         // Terminador nulo

    mov x0, x9             // Dirección del string en x0
    bl print_string

    // Salto de línea
    mov x0, #1
    adr x1, newline
    mov x2, #1
    mov x8, #64
    svc #0

    // Salida limpia
    mov x0, #0
    mov x8, #93
    svc #0

// -------------------------------
// print_string (x0 contiene la dirección)
// -------------------------------
print_string:
    stp x29, x30, [sp, #-16]!
    stp x19, x20, [sp, #-16]!

    mov x19, x0

1:
    ldrb w20, [x19]
    cbz w20, 2f

    mov x0, #1
    mov x1, x19
    mov x2, #1
    mov x8, #64
    svc #0

    add x19, x19, #1
    b 1b

2:
    ldp x19, x20, [sp], #16
    ldp x29, x30, [sp], #16
    ret
