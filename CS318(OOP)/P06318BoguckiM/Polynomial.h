// Polynomial.h -- starter code
#ifndef __POLYNOMIAL_H_
#define __POLYNOMIAL_H_
#include <iostream>
#include <list>
using namespace std;

///////////////////////////////////////////////////////// Monomial Class
// Monomial - class that creates the terms that get inserted in a Polynomial
//	-- DO NOT MODIFY
template <typename NumT>
class Monomial {
public:
	Monomial<NumT>(NumT c = 0, int d = 0) : coeff(c), expo(d) { };
	NumT coefficient(void) const { return coeff; };
	int degree(void) const { return expo; };
	void assign_coefficient(const NumT c) { coeff = c; };
	void assign_degree(const int d) { expo = d; };

	bool operator==(const Monomial<NumT>& m) const {
		return (coeff == m.coeff && expo == m.expo);
	}

	bool operator!=(const Monomial<NumT>& m) const {
		return (coeff != m.coeff || expo != m.expo);
	}
private:
	NumT coeff; // in our case this will be an int
	int	expo;
};

//////////////////////////////////////////////////////Polynomial Class
template <typename NumberType>
class Polynomial
{
public:
	// Default constructor -- DO NOT MODIFY
	Polynomial<NumberType>(NumberType c = 0, int d = 0) {
		const Monomial<NumberType> m(c, d);
		term_list.push_back(m); //creates at least one monomial
		number_of_terms = 1;
		highest_degree = d;
	}

	// Type conversion construct -- DO NOT MODIFY
	Polynomial<NumberType>(const Monomial<NumberType>& m) {
		term_list.push_back(m);
		number_of_terms = 1;
		highest_degree = m.degree();
	}

	// Destructor - use default destructors and list's destructor -- DO NOT MODIFY
	~Polynomial<NumberType>() { term_list.clear();  }

	// Copy Constructor -- DO NOT MODIFY
	Polynomial<NumberType>(const Polynomial<NumberType>& rhs)
		: term_list(rhs.term_list),
		number_of_terms(rhs.number_of_terms),
		highest_degree(rhs.highest_degree) {}

	int gethighestdegree() const { return highest_degree; }

	// COMPLETE ALL OF THE FOLLOWING
	const Polynomial<NumberType>& operator=(const Polynomial<NumberType>& rhs) { // copy assignment
		if (this != &rhs) {
			term_list = rhs.term_list;
			number_of_terms = rhs.number_of_terms;
			highest_degree = rhs.highest_degree;
		}
		return *this;
	}
	const Polynomial<NumberType>& operator=(Polynomial<NumberType>&& rhs) { // move assignment
		if (this != &rhs) {
			term_list = std::move(rhs.term_list);
			number_of_terms = rhs.number_of_terms;
			highest_degree = rhs.highest_degree;

			rhs.number_of_terms = 0;
			rhs.highest_degree = 0;
		}
		return *this;
	}

	// ADDITION
	Polynomial<NumberType> operator+=(const Monomial<NumberType>& m) { //1
		insert_in_poly(*this, m);
		return *this;
	}
	Polynomial<NumberType> operator+=(const Polynomial<NumberType>& rhs) { //2
		for (const auto& term : rhs.term_list) {
			insert_in_poly(*this, term);
		}
		return *this;
	}
	const Polynomial<NumberType> operator+ (const Monomial<NumberType>& m)const { //3
		Polynomial<NumberType> result(*this);
		result += m;
		return result;
	}
	const Polynomial<NumberType> operator+ (const Polynomial<NumberType>& rhs) const { //4
		Polynomial<NumberType> result(*this);
		result += rhs;
		return result;
	}

	// SUBTRACTION
	Polynomial<NumberType> operator-=(const Monomial<NumberType>& m) {
		Monomial<NumberType> neg_m(-m.coefficient(), m.degree());
		*this += neg_m;
		return *this;
	}
	Polynomial<NumberType> operator-=(const Polynomial<NumberType>& rhs) {
		for (const auto& term : rhs.term_list) {
			Monomial<NumberType> neg_term(-term.coefficient(), term.degree());
			insert_in_poly(*this, neg_term);
		}
		return *this;
    }
	const Polynomial<NumberType> operator- (const Monomial<NumberType>& m)const {
		Polynomial<NumberType> result(*this);
		result -= m;
		return result;
	}
	const Polynomial<NumberType> operator- (const Polynomial<NumberType>& rhs) const {
		Polynomial<NumberType> result(*this);
		result -= rhs;
		return result;
	}

	// MULTIPLICATION
	Polynomial<NumberType> operator*=(const Monomial<NumberType>& m) {
		for (auto& term : term_list) {
			term.assign_coefficient(term.coefficient() * m.coefficient());
			term.assign_degree(term.degree() + m.degree());
		}
		term_list.remove_if([](const Monomial<NumberType>& term) { return term.coefficient() == 0; });
		return *this;
	}
	Polynomial<NumberType> operator*=(const Polynomial<NumberType>& rhs) {
		Polynomial<NumberType> result(0, 0);

		for (const auto& lhs_term : term_list) {
			for (const auto& rhs_term : rhs.term_list) {
				Monomial<NumberType> new_term(lhs_term.coefficient() * rhs_term.coefficient(),
					lhs_term.degree() + rhs_term.degree());
				result += new_term;
			}
		}

		*this = std::move(result);
		return *this;
	}
	const Polynomial<NumberType> operator*(const Monomial<NumberType>& m) const {
		Polynomial<NumberType> result(*this);
		result *= m;
		return result;
	}
	const Polynomial<NumberType> operator*(const Polynomial<NumberType>& rhs)const {
		Polynomial<NumberType> result(*this);
		result *= rhs;
		return result;
	}

	// EVALUATION POLYNOMIAL
	const NumberType evaluate(NumberType x) const {
		NumberType result = 0;
		for (const auto& term : term_list) {
			result += term.coefficient() * power(x, term.degree());
		}
		return result;
	}

	// EQUALITY TESTS
	bool operator==(const Polynomial<NumberType>& p) const {
		return term_list == p.term_list;
	}
	bool operator!=(const Polynomial<NumberType>& p) const {
		return !(*this == p);
	}

	// CALLED FROM ISTREAM AND OSTREAM OVERLOADS
	// read() function has responsibility for getting input, creating Monomial object,
	//			and inserting into Polynomial object
	void read(istream& in = cin) {
		term_list.clear();
		number_of_terms = 0;
		highest_degree = 0;

		NumberType coeff;
		int degree;

		while (in >> coeff && coeff != 0) {
			if (!(in >> degree)) break;
			Monomial<NumberType> m(coeff, degree);
			insert_in_poly(*this, m);
		}
	}

	// print() function -- make sure you look back at requirements for this
	void print(ostream& out = cout) const {
		if (term_list.empty()) {
			out << endl << "Number terms 0" << endl;
			return;
		}

		out << endl;
		out << "Number terms " << number_of_terms << endl;

		bool isFirst = true;
		for (const auto& term : term_list) {
			if (term.coefficient() == 0) continue; 

			if (isFirst) {
				if (term.coefficient() == -1 && term.degree() != 0) {
					out << "-";
				}
				else if (term.coefficient() != 1 || term.degree() == 0) {
					out << term.coefficient();
				}
				isFirst = false;
			}
			else {
				if (term.coefficient() > 0) {
					out << " + ";
					if (term.coefficient() != 1 || term.degree() == 0) {
						out << term.coefficient();
					}
				}
				else {
					out << " - ";
					if (term.coefficient() != -1 || term.degree() == 0) {
						out << -term.coefficient();
					}
				}
			}

			if (term.degree() != 0) {
				out << "x";
				if (term.degree() != 1) {
					out << "^" << term.degree();
				}
			}
		}

		out << endl;
	}

private:
	list<Monomial<NumberType> > term_list; //SORTED BY DECREASING DEGREES
	int number_of_terms;
	int highest_degree;

	//private helper member function -- review notes below function prototype
	void insert_in_poly(Polynomial<NumberType>& p, const Monomial<NumberType>& m) {
		if (m.coefficient() == 0) return; 

		for (auto it = p.term_list.begin(); it != p.term_list.end(); ++it) {
			if (it->degree() == m.degree()) {
				NumberType new_coeff = it->coefficient() + m.coefficient();
				if (new_coeff == 0) {
					p.term_list.erase(it);
					p.number_of_terms--;
				}
				else {
					it->assign_coefficient(new_coeff); 
					p.highest_degree = max(p.highest_degree, it->degree());
				}
				return;
			}
			else if (it->degree() < m.degree()) {
				p.term_list.insert(it, m);
				p.number_of_terms++;
				p.highest_degree = max(p.highest_degree, m.degree());
				return;
			}
		}

		p.term_list.push_back(m); 
		p.number_of_terms++;
		p.highest_degree = max(p.highest_degree, m.degree());
	}

	// Used in evaluate member function
	NumberType power(NumberType x, int n) const {
		NumberType result = 1;
		for (int i = 0; i < n; ++i) {
			result *= x;
		}
		return result;
	}
};

// DO NOT MODIFY
template<typename NumberType>
istream& operator>>(istream& in, Polynomial<NumberType>& rhs) {
	rhs.read();
	return in;
}

// DO NOT MODIFY
template<typename NumberType>
ostream& operator<<(ostream& out, const  Polynomial<NumberType>& rhs) {
	rhs.print();
	return out;
}

#endif