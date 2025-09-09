#include "complex.h"

complex::complex() : re(0), im(0) {}
complex::complex(double a) : re(a), im(0) {}
complex::complex(double a, double b) : re(a), im(b) {}
complex::complex(const complex& c) : re(c.re), im(c.im) {}

complex& complex::operator=(const complex& rhs) {
    if (this != &rhs) {
        re = rhs.re;
        im = rhs.im;
    }
    return *this;
}

complex& complex::operator+=(const complex& z) {
    re += z.re;
    im += z.im;
    return *this;
}

complex complex::operator-=(const complex& z) {
    re -= z.re;
    im -= z.im;
    return *this; 
}

complex complex::operator*=(const complex& z) {
    double real = re * z.re - im * z.im;
    double imag = re * z.im + im * z.re;
    re = real;
    im = imag;
    return *this;
}

complex complex::operator/=(const complex& z) {
    if (z.re == 0 && z.im == 0) {
        throw invalid_argument("Can't divide by 0");
    }

    double denominator = (z.re * z.re) + (z.im * z.im);
    double temp = re;
    re = (re * z.re + im * z.im) / denominator; 
    im = (im * z.re - temp * z.im) / denominator; 
    return *this;
}

double complex::real() const {
    return re;
}

double complex::imag() const {
    return im;
}

double complex::magnitude() const {
    return sqrt(re * re + im * im);
}

complex operator+(const complex& z1, const complex& z2) {
    complex result = z1;
    result += z2;
    return result;
}

complex operator+(const complex& z1, const double x) {
    complex result = z1;
    result += complex(x);
    return result;
}

complex operator+(const double x, const complex& z) {
    return z + x;
}

complex operator-(const complex& z1, const complex& z2) {
    complex result = z1;
    result -= z2;
    return result;
}

complex operator-(const complex& z1, const double x) {
    complex result = z1;
    result -= complex(x);
    return result;
}

complex operator-(const double x, const complex& z) {
    complex result(x);
    result -= z;
    return result;
}

complex operator*(const complex& z1, const complex& z2) {
    complex result = z1;
    result *= z2;
    return result;
}

complex operator*(const complex& z1, const double x) {
    complex result = z1;
    result *= complex(x);
    return result;
}

complex operator*(const double x, const complex& z) {
    return complex(x) * z;
}

complex operator/(const complex& z1, const complex& z2) {
    complex result = z1;
    result /= z2;
    return result;
}

complex operator/(const complex& z1, const double x) {
    complex result = z1;
    result /= complex(x);
    return result;
}

complex operator/(const double x, const complex& z) {
    complex result(x);
    result /= z;
    return result;
}

complex operator+(const complex& z) {
    return z;
}

complex operator-(const complex& z) {
    return complex(-z.real(), -z.imag());
}

bool operator==(const complex& z1, const complex& z2) {
    return (z1.real() == z2.real() && z1.imag() == z2.imag());
}

bool operator!=(const complex& z1, const complex& z2) {
    return !(z1 == z2);
}

istream& operator>>(istream& is, complex& z) { 
    double a, b;
    char c1, c2, c3;

    is >> c1 >> a >> c2 >> b >> c3;
    z = complex(a, b);
    return is;
}

ostream& operator<<(ostream& os, const complex& z) { 
    os << "(" << z.real() << ", " << z.imag() << ")";
    return os;
}

double magnitude(const complex& z) { 
    return sqrt(z.real() * z.real() + z.imag() * z.imag());
}

double real(const complex& z) {
    return z.real();
}

double imag(const complex& z) {
    return z.imag();
}

complex polar(const double r, const double theta) { 
    return complex(r * cos(theta), r * sin(theta));
}

complex polar(const double r) {
    return complex(r, 0);
}

complex conj(const complex& z) { 
    return complex(z.real(), -z.imag());
}

double norm(const complex& z) { 
    return z.real() * z.real() + z.imag() * z.imag();
}

double arg(const complex& z) { 
    if (z.real() == 0 && z.imag() == 0) {
        throw invalid_argument("Argument undefined for z = 0");
    }
    return atan2(z.imag(), z.real());
}