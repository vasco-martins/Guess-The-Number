Module Module1

    Dim player1, player2 As Boolean 'Os números dos diferentes jogadores
    Dim currentPlayer As Integer = 1 'Aquele que está a jogar

    Sub Main()
        Dim guessNumber As String
        Dim bol As Boolean

        Do

            Console.Clear()
            Console.WriteLine("============ Jogador {0} ============", currentPlayer)
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
    ' @params toguess -> Número a ser adivinhado
    ' @params player -> Jogador que vai tentar adivinhar
    ' @Retorna False caso o jogo deva continuar
    ' @Retorna True caso o jogo deva parar
    Function Guess(toguess As String, player As Integer) As Boolean
        Dim flag As Boolean = False
        Dim tries, correctNums As Integer
        Dim tryguess, failedPositions As String
        Dim show(5), tryGuessArray(5), ToGuessArray(5) As Char
        tries = 0

        Do

            ' Nesta variável vamos armazenar todos os números que o utilizador introduziu numa posição errada
            failedPositions = ""

            ToGuessArray = toguess.ToCharArray

            Console.Clear()

            ' Nas seguintes linhas, para tornar tudo muito mais bonitinho e amigo, vamos simplesmente mostrar diretamente a animaçãO
            ' (Basicamente cancelamos a animação, passando o '0' para o threading.thread.sleep())
            If (tries <> 0) Then
                showGuessingAnimation(show, 0)
            End If


            Console.WriteLine("Tentativa {0}", tries + 1)
            tryguess = readNumber("Player " + player.ToString + ", por favor digite um número: ")

            ' Passamos todos os númerors digitados para um array de caracteres
            ' Assim, será muito mais simples fazer a verificação dos números
            tryGuessArray = tryguess.ToCharArray()
            Console.WriteLine()

            If (tryguess = toguess) Then ' O jogador acertou!
                flag = True
            Else ' O jogador não acertou
                tries += 1
                correctNums = 0

                ' Verifica se o utilizador acertou em algum algarismo :)
                For i As Integer = 0 To 4

                    If (toguess.ToCharArray()(i) = tryGuessArray(i)) Then
                        correctNums += 1
                        show(i) = toguess.ToCharArray()(i)
                        ToGuessArray(i) = "Z"
                    Else

                        ' Caso o jogador não tenha acertado nada vai preencher tudo com "X"
                        ' Coitadinho do jogador :(
                        show(i) = "X"
                    End If

                Next

                For i As Integer = 0 To 4
                    ' Caso o uilizador acerte num número mas numa posição diferente vai adicionar à string
                    If (ToGuessArray.Contains(tryGuessArray(i))) Then
                        failedPositions += tryGuessArray(i) + " "
                    End If
                Next

                ' Caso o utilizador não tenha acertado em nenhuma posição :(
                If (correctNums = 0) Then
                    Console.WriteLine("Não acertou em nenhuma posição")
                Else
                    ' Vai renderizar a animação pipi, em 4,5 segundos (450 ms)
                    showGuessingAnimation(show, 450)
                End If

                Console.WriteLine("")

                If (failedPositions <> "") Then
                    Console.WriteLine("Números em que errou a posição: " + failedPositions)
                End If

                Console.WriteLine()
                Console.Write("Clique Enter para continuar")
                Console.ReadKey()
            End If

        Loop Until (tries = 10 Or flag)

        Console.Clear()

        ' Caso o jogador tenha adivinhado o númearo
        If (flag = True) Then

            Console.WriteLine()
            Console.WriteLine("       Parabéns! Você adivinhou!")
            Console.WriteLine()
            Console.Write("Clique Enter para continuar")
            Console.ReadKey()

            ' Caso o 1º jogador ganhe, vamos mudar para o próximo
            ' currentPlayer -> passa  a ser o 2
            If (currentPlayer = 1) Then
                player1 = True
                currentPlayer = 2
                Return False
            Else
                'O mesmo processo será executado, mas para caso seja o 2º jogador
                player2 = True
            End If
        Else
            Console.WriteLine("Acabaram-se as tentativas, perdeu o jogo!")
            Console.WriteLine("A sequência correta era: {0}", toguess)
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

        ' Vai mostrar quem ganhou e quem perdeu
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
        If (wannaAnother()) Then
            currentPlayer = 1
            Return False
        Else
            Return True
        End If

        ' Por defeito vai retornar False
        Return False

    End Function

    ' Lê o número a ser adivinhado
    Function readNumber(message As String) As String
        Dim i As String
        Dim typed As Integer
        Dim flag As Boolean = True
        Do
            Try
                flag = True
                Console.Write(message)
                i = Console.ReadLine()

                Try
                    ' Com esta variável "typed" vamos converter e armazenar o número digitado para Integer
                    typed = CInt(i)
                Catch ex As Exception
                    flag = False
                    Console.WriteLine("Digite apenas números")
                End Try

                ' Vamos verificar se a length do número é diferente de 5
                ' Sim, okay, podiamos estar neste momento a converter para uma string e bla bla bla, mas como somos reles programadores experientes declaramos como String
                If (i.Length <> 5) Then
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
                    flag = True
                ElseIf (i.ToLower = "n" Or i.ToLower() = "não" Or i.ToLower() = "nao") Then
                    ret = False
                    flag = True
                Else
                    ' Vamos mostrar que o jogador introduziu uma resposta errada
                    Console.WriteLine("")
                    flag = False
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

    ' Temos aqui um procedimento que simplesmente vai carregar a animação pipi
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