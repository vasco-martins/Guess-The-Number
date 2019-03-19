Module Module1

    Dim player1, player2 As Boolean 'Os números dos diferentes jogadores
    Dim currentPlayer As Integer = 1 'Aquele que está a jogar

    Sub Main()
        Dim guessNumber As String
        Dim bol As Boolean

        Do
            Console.Clear()
            Console.WriteLine("============ PLAYER {0} ============", currentPlayer)
            Console.WriteLine("")
            guessNumber = readNumber("Digite o número a ser adivinhado: ")
            Console.Clear()

            If (currentPlayer = 1) Then
                bol = Guess(guessNumber, 2)
            Else
                bol = Guess(guessNumber, 1)
            End If



        Loop Until (bol)

        Console.ReadKey()
    End Sub


    ' Função principal do jogo
    ' Recebe como parâmetros o número a ser adivinhado e jogador que vai tentar adivinhar
    ' Retorna False caso o jogo deva continuar, e True caso o jogo deva parar
    Function Guess(gnum As String, player As Integer) As Boolean
        Dim tries, correctNums As Integer
        Dim guessing_number As String
        Dim flag As Boolean = False

        Dim show(5) As Char

        tries = 0

        Do

            Console.Clear()

            If (tries <> 0) Then
                showGuessingAnimation(show, 0)
            End If


            Console.WriteLine("Tentativa {0}", tries + 1)
            guessing_number = readNumber("Player " + player.ToString + ", por favor digite um número: ")
            Console.WriteLine()

            If (guessing_number = gnum) Then ' O jogador acertou!
                flag = True
            Else ' O jogador não acertou
                tries += 1
                correctNums = 0

                ' Verifica se o utilizador acertou em algum algarismo :)
                For i As Integer = 0 To 4
                    If (gnum.ToCharArray()(i) = guessing_number.ToCharArray()(i)) Then
                        correctNums += 1
                        show(i) = gnum.ToCharArray()(i)
                    Else
                        show(i) = "X"
                    End If
                Next

                ' Caso o utilizador não tenha acertado em nenhuma posição :(
                If (correctNums = 0) Then
                    Console.WriteLine("Não acertou em nenhuma posição")
                Else
                    showGuessingAnimation(show, 450)
                End If

                Console.WriteLine()
                Console.Write("Clique Enter para continuar")
                Console.ReadKey()
            End If

        Loop Until (tries = 10 Or flag)

        Console.Clear()
        ' Caso o jogador tenha adivinhado o númearo
        If (flag = True) Then

            Console.WriteLine("Parabéns! Você adivinhou!")

            ' Caso o 1º jogador ganhe, vamos mudar para o próximo, defenindo a variável player1 e current player e retornar True, para indica que este ganhou
            If (currentPlayer = 1) Then
                player1 = True
                currentPlayer = 2
                Return False
            Else 'O mesmo processor será executado, mas para caso seja o 2º jogador
                player2 = True
            End If
        Else
            Console.WriteLine("Acabaram-se as tentativas, perdeu o jogo!")
            Console.WriteLine("A sequência correta era: {0}", gnum)
            Console.ReadKey()

            If (currentPlayer = 1) Then
                player1 = False
                Return False
            Else
                currentPlayer = 2
                player2 = False
            End If
        End If

        Console.Clear()
        ' Vencedor
        If (player1 And player2) Then
            Console.WriteLine("Foi um empate!")
        ElseIf (player1) Then
            Console.WriteLine("O jogador 1 venceu!")
        ElseIf (player2) Then
            Console.WriteLine("O jogador 2 venceu!")
        Else
            Console.WriteLine("Ninguém Venceu :(")
        End If

        'Verificar se os jogadores querem jogar de novo e caso o return seja true, vamos continuar...
        If (wannaAnother()) Then 'wannaAnother() está na linha 144
            currentPlayer = 1
            Return False
        Else
            Return True
        End If

        Return False

    End Function

    ' Lê o número a ser adivinhado
    Function readNumber(message As String) As String
        Dim i As String
        Dim a As Integer
        Dim flag As Boolean = True
        Do
            Try
                flag = True
                Console.Write(message)
                i = Console.ReadLine()

                Try
                    a = CInt(i)
                Catch ex As Exception
                    flag = False
                    Console.WriteLine("Digite apenas números")

                End Try
                ' Vamos verificar se a length do número é diferente de 5 
                If (i.Length <> 5) Then ' Sim, okay, podiamos estar neste momento a converter para uma string e bla bla bla, mas como somos reles programadores experientes declaramos como String
                    flag = False
                    Console.WriteLine("O número deverá conter 5 digitos")
                    Console.ReadKey()
                    Console.Clear()
                End If
            Catch ex As Exception 'Caso o utilizador digite algo que não é um número
                flag = False
                Console.WriteLine("O que introduziu não é um número")
            End Try
        Loop Until (flag)

        Return i

    End Function

    Function wannaAnother() As Boolean
        Dim i As String
        Dim flag As Boolean = True
        Dim ret As Boolean
        Do
            Try
                flag = True
                Console.Write("Deseja continuar? [S/N]: ")
                i = Console.ReadLine

                If (i.ToLower = "s" Or i.ToLower() = "sim") Then
                    ret = True
                Else
                    ret = False
                End If
            Catch ex As Exception
                flag = False
                Console.WriteLine("Tipo de dados inválido")
                Console.ReadKey()
                Console.Clear()
            End Try
        Loop Until (flag)

        Return ret
    End Function

    Sub showGuessingAnimation(vect() As Char, time As Integer)
        Dim line As String = "        "

        For i As Integer = 0 To 4
            Console.Clear()

            line += vect(i) + "    "

            Console.WriteLine("======================================")
            Console.WriteLine("")
            Console.WriteLine(line)
            Console.WriteLine("")
            Console.WriteLine("======================================")

            Threading.Thread.Sleep(time)
        Next

    End Sub


End Module
