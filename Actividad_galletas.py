class Galleta:
    def __init__(self, sabor):
        self.sabor = sabor

    def sabor(self):
        return f"el sabor es {self.sabor}"
    
s = Galleta("vainilla")
print(s.sabor)

g1 = Galleta("chocolate")
g2 = Galleta("chocolate")

print (g1 is g2)
print(g1.sabor == g2.sabor)
