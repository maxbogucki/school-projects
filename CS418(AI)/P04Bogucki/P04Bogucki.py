import constraint
from constraint import AllDifferentConstraint

problem = constraint.Problem()

ages    = [109, 110, 111, 112, 113]
persons = ["Iris", "Larry", "Shari", "Victor", "Noel"]
states  = ["Delaware", "Florida", "Hawaii", "Illinois", "Kansas"]
towns   = ["Jackson", "Kirkville", "Rutherford", "Villa Park", "Yountville"]

# Add each category’s items as variables over the domain of ages
for category in (persons, states, towns):
    problem.addVariables(category, ages)
    problem.addConstraint(AllDifferentConstraint(), category)

# Constraints

# 1. The 110-year-old doesn’t live in Yountville
problem.addConstraint(lambda y: y != 110, ("Yountville",))

# 2. Neither the Villa Park resident nor Iris is the 109-year-old
problem.addConstraint(lambda v: v != 109, ("Villa Park",))
problem.addConstraint(lambda i: i != 109, ("Iris",))
problem.addConstraint(lambda v, i: v != i, ("Villa Park", "Iris"))

# 3. The Kansas native is 1 year younger than Iris
problem.addConstraint(lambda ks, ir: ks + 1 == ir, ("Kansas", "Iris"))

# 4. Larry is a native of Hawaii
problem.addConstraint(lambda l, h: l == h, ("Larry", "Hawaii"))

# 5. The 112-year-old is a native of Delaware
problem.addConstraint(lambda d: d == 112, ("Delaware",))

# 6. Of Iris and the 113-year-old, one is Florida and the other Illinois
problem.addConstraint(
    lambda fl, il, ir: (fl == ir and il == 113) or (fl == 113 and il == ir),
    ("Florida", "Illinois", "Iris")
)

# 7. Victor is 2 years younger than the centenarian who lives in Jackson
problem.addConstraint(lambda v, j: v + 2 == j, ("Victor", "Jackson"))

# 8. Noel lives in Rutherford
problem.addConstraint(lambda n, r: n == r, ("Noel", "Rutherford"))

# 9. The person who lives in Yountville isn’t a native of Illinois
problem.addConstraint(lambda y, il: y != il, ("Yountville", "Illinois"))

# 10. The 109-year-old lives in Kirkville
problem.addConstraint(lambda k: k == 109, ("Kirkville",))

# Solve and print solution
solutions = problem.getSolutions()

for idx, sol in enumerate(solutions, start=1):
    print(f"🧩 Solution:")
    for age in sorted(ages):
        name  = next(p for p in persons if sol[p] == age)
        state = next(s for s in states  if sol[s] == age)
        town  = next(t for t in towns   if sol[t] == age)
        print(f"  {age} years, {name}, {state}, {town}")