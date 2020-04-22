Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = target_fun_1(_618fun(0, 1, 0.2))
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox3.Text = target_fun_1(_fibonaccifun(0, 1, 0.2))
    End Sub
    Private Function target_fun_1(x As Double) As Double
        target_fun_1 = Math.Exp(-x) + x ^ 2
        Return target_fun_1
    End Function
    Private Function _618fun(a As Double, b As Double, L As Double)
        Dim lambda, mu, a1, b1, lambda1, mu1 As Double
        lambda = a + 0.382 * (b - a)
        mu = a + 0.618 * (b - a)
        While ((b - a) > L)
            If (target_fun_1(lambda) > target_fun_1(mu)) Then
                a1 = lambda
                b1 = b
                lambda1 = mu
                mu1 = a1 + 0.618 * (b1 - a1)
            Else
                a1 = a
                b1 = mu
                mu1 = lambda
                lambda1 = a1 + 0.382 * (b1 - a1)
            End If
            a = a1
            b = b1
            lambda = lambda1
            mu = mu1
        End While
        _618fun = (b + a) / 2
        Return _618fun
    End Function
    Private Function _fibonaccifun(a As Double, b As Double, L As Double)
        Dim lambda, mu As Double
        Dim Fib(100) As Integer
        Dim n, k As Integer
        Fib(0) = 1
        Fib(1) = 1
        Fib(2) = 2
        For k = 3 To 100
            Fib(k) = Fib(k - 1) + Fib(k - 2)
            If Fib(k) > ((b - a) / L) Then
                n = k
                Exit For
            End If
        Next k
        lambda = a + Fib(n - 2) * (b - a) / Fib(n)
        mu = a + Fib(n - 1) * (b - a) / Fib(n)
        For k = 2 To (n - 1)
            If (target_fun_1(lambda) > target_fun_1(mu)) Then
                a = lambda
                lambda = mu
                mu = a + (Fib(n - k) / Fib(n - k + 1)) * (b - a)
            Else
                b = mu
                mu = lambda
                lambda = a + (Fib(n - k - 1) / Fib(n - k + 1)) * (b - a)
            End If
        Next k
        Return (b + a) / 2
    End Function
    Private Function target_fun_2(x As Double) As Double
        target_fun_2 = 3 * (x ^ 4) - 4 * (x ^ 3) - 12 * (x ^ 2)
        Return target_fun_2
    End Function
    Private Function dif(x As Double) As Double
        Dim dx = 0.000001
        Dim dy = target_fun_2(x + dx) - target_fun_2(x)
        dif = dy / dx
        Return dif
    End Function
    Private Function ddif(x As Double) As Double
        Dim dx = 0.000001
        Dim ddy = target_fun_2(x + dx) + target_fun_2(x - dx) - target_fun_2(x) * 2
        ddif = ddy / dx / dx
        Return ddif
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs)

    End Sub
    Private Function Newton(x0 As Double, k As Double) As Double
        Dim i As Integer
        For i = 1 To k
            x0 = x0 - dif(x0) / ddif(x0)
        Next i
        Return x0
    End Function
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox5.Text = target_fun_2(Newton(-1.2, 3))
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox6.Text = target_fun_2(secant(-1.2, -0.8, 3))
    End Sub
    Private Function secant(x1 As Double, x2 As Double, k As Double)
        Dim i As Integer
        Dim x3 As Double
        For i = 1 To k
            x3 = x2 - (x2 - x1) * dif(x2) / (dif(x2) - dif(x1))
            x1 = x2
            x2 = x3
        Next
        Return x2
    End Function
    Private Sub TextBox5_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox8.Text = target_fun_3(conjugate_gradient(2, 0, -4, 0, 4, -4, 1, 3))
    End Sub

    Private Function target_fun_3(x() As Double) As Double
        Dim x1 = x(0)
        Dim x2 = x(1)
        target_fun_3 = (x1 - 2) ^ 2 + 2 * (x2 - 1) ^ 2
        TextBox11.Text = "x1=" + x1.ToString() + ", x2=" + x2.ToString()
        Return target_fun_3
    End Function
    '梯度计算
    Private Function gradient(x1 As Double, x2 As Double, a As Double, b As Double, c As Double, d As Double, e As Double, f As Double)
        Dim y1 = a * x1 + b * x2 + c
        Dim y2 = d * x1 + e * x2 + f
        Dim y(2) As Double
        y(0) = y1
        y(1) = y2
        Return y
    End Function
    '共轭梯度法
    Private Function conjugate_gradient(a As Double, b As Double, c As Double, d As Double, e As Double, f As Double, x1 As Double, x2 As Double)
        '1/2xTAx+bTx+c
        Dim g(2) As Double

        Dim g11 As Double, g12 As Double, g21 As Double, g22 As Double
        Dim A11 As Double, A12 As Double, A21 As Double, A22 As Double
        Dim k As Integer
        Dim lambda As Double, beta As Double
        Dim d1 As Double, d2 As Double
        Dim M(2) As Double

        A11 = a
        A12 = b
        A21 = d
        A22 = e
        g = gradient(x1, x2, a, b, c, d, e, f) '梯度计算
        g11 = g(0)
        g12 = g(1)
        If g11 = 0 Or g12 = 0 Then
            M(0) = x1
            M(1) = x2
            Return M
        Else
            d1 = -g11
            d2 = -g12

            lambda = -(g11 * d1 + g12 * d2) / (d1 * (d1 * A11 + d2 * A21) + d2 * (d1 * A12 + d2 * A22))

            x1 = x1 + lambda * d1
            x2 = x2 + lambda * d2

            For k = 1 To 100
                g = gradient(x1, x2, a, b, c, d, e, f)
                g21 = g(0)
                g22 = g(1)
                If g21 = 0 Or g22 = 0 Then Exit For
                beta = ((d1 * A11 + d2 * A21) * g21 + (d1 * A12 + d2 * A22) * g22) / ((d1 * A11 + d2 * A21) * d1 + (d1 * A12 + d2 * A22) * d2)

                d1 = -g21 + beta * d1
                d2 = -g22 + beta * d2

                lambda = -(g21 * d1 + g22 * d2) / (d1 * (d1 * A11 + d2 * A21) + d2 * (d1 * A12 + d2 * A22))
                x1 = x1 + lambda * d1
                x2 = x2 + lambda * d2

                g11 = g21
                g12 = g22
            Next k
            M(0) = x1
            M(1) = x2
            Return M
        End If
    End Function

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox9.Text = target_fun_4(conjugate_gradient(4, 2, 3, 2, 2, -4, 3, 4))
    End Sub
    Private Function target_fun_4(x() As Double) As Double
        Dim x1 = x(0)
        Dim x2 = x(1)
        target_fun_4 = 2 * (x1 ^ 2) + 2 * x1 * x2 + x2 ^ 2 + 3 * x1 - 4 * x2
        TextBox12.Text = "x1=" + x1.ToString() + ", x2=" + x2.ToString()
        Return target_fun_4
    End Function

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox10.Text = target_fun_5(conjugate_gradient(4, 2, 0, 2, 10, 0, 2, -2))
    End Sub
    Private Function target_fun_5(x() As Double) As Double
        Dim x1 = x(0)
        Dim x2 = x(1)
        target_fun_5 = 2 * (x1 ^ 2) + 2 * x1 * x2 + 5 * (x2 ^ 2)
        TextBox13.Text = "x1=" + x1.ToString() + ", x2=" + x2.ToString()
        Return target_fun_5
    End Function
End Class
