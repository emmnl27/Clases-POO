class Galleta:
    def __init__(self, sabor):
        self.sabor = sabor

g1 = Galleta("chocolate")
g2 = Galleta("chocolate")

print (g1 is g2)
print(g1.sabor == g2.sabor)
