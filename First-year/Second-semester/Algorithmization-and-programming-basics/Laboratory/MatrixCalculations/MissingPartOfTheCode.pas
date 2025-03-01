Function Sum (x, y: TMatrix) : TMatrix;

//x, y - input matrices

var i, j : Integer;

begin
//adds each element
  for i := 1 to 3 do
    for j := 1 to 3 do
      result[i,j] := x[i,j] + y[i,j]
end;

(*following function calculates Sub of matrices*)
Function Sub (x, y: TMatrix) : TMatrix;

//x, y - input matrices

var i, j : Integer;

begin
//subtracts from each element of first matrix element from second matrix
  for i := 1 to 3 do
    for j := 1 to 3 do
      result[i,j] := x[i,j] - y[i,j]
end;

(*following function calculates Mul of matrices*)
Function MulArr (x, y: TMatrix) : TMatrix;

//x, y - input matrices

var i, j, k : Integer;

begin
//product of matrices is satisfied
  for i := 1 to 3 do
    for j := 1 to 3 do
    begin

//initializing
      result[i,j] := 0;
      for k := 1 to 3 do
        result[i,j] := result[i,j] + x[i,k] * y[k,j]
    end
end;

(*following function calculates product of matrix by num*)
Function MulConst (x: Integer; y: TMatrix) : TMatrix;

//x - input num
//y - input matrix

var i, j : Integer;

begin
//product of each element of matrix by num
  for i := 1 to 3 do
    for j := 1 to 3 do
      result[i,j] := x * y[i,j]
end;

