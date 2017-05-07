# Numerical-Methods
C# Implementation of Basic Numerical Methods

##LinearAlgebraicEquation
This class contains static functions to find roots of n linear equations.

### Properties
<dl>
<dt>MaxError:</dt><dd> Maximum allowed error ratio for iterative calculations</dd>
<dt>MaxIteration:</dt><dd> Maximum number of iterations for iterative calculations</dd>
<dt>Lambda:</dt><dd> Value of Lambda for Gauss Seidel formula</dd>
</dl>
### Function Listing
<ul>
<li><a href="#naive-gauss">Naive Gauss(double[][], double[]) </a> </li>
<li><a href="#gauss-seidel">Gauss Seidel(double[][], double[]) </a> </li>
<li><a href="#gauss-jordan">Gauss Jordan(double[][], double[]) </a> </li>
<li><a href="#gauss-jordan-overload">Gauss Jordan(double[][]) </a> </li>
<li><a href="#ludecomposition">LUDecomposition(double[][], double[]) </a> </li>
</ul>

### Function Definition
#### Naive Gauss
<dl>
<dt> Parameter: </dt>
<dd> 
double[][] a : Matrix of co-efficients
<br/>
double[] b : Array of constants
</dd>
<dt>Returns</dt>
<dd>double[] : Array of roots solved using <em>Naive Gauss</em> Formula
</dl>
#### Gauss Seidel
<dl>
<dt> Parameter: </dt>
<dd> 
double[][] a : Matrix of co-efficients
<br/>
double[] b : Array of constants
</dd>
<dt>Returns</dt>
<dd>double[] : Array of roots solved using <em>Gauss Seidel</em> Formula
</dl>
#### Gauss Jordan
<dl>
<dt> Parameter: </dt>
<dd> 
double[][] a : Matrix of co-efficients
<br/>
double[] b : Array of constants
</dd>
<dt>Returns</dt>
<dd>double[] : Array of roots solved using <em>Gauss Jordan</em> Formula
</dl>
#### Gauss Jordan (Overload)
<dl>
<dt> Parameter: </dt>
<dd> 
double[][] a : Matrix of co-efficients and constant
</dd>
<dt>Returns</dt>
<dd>double[] : Array of roots solved using <em>Gauss Jordan</em> Formula
</dl>
#### LUDecomposition
<dl>
<dt> Parameter: </dt>
<dd> 
double[][] a : Matrix of co-efficients
<br/>
double[] b : Array of constants
</dd>
<dt>Returns</dt>
<dd>double[] : Array of roots solved using <em>LU Decomposition with Gauss Elimination</em> Formula
</dl>
