unit UnitMatrixArithmetic;

interface
  type
   TMatrix = array [1..3] of array [1..3] of Integer;
   
   Function Sum (x, y: TMatrix) : TMatrix;
   Function Sub (x, y: TMatrix) : TMatrix;
   Function MulArr (x, y: TMatrix) : TMatrix;
   Function MulConst (x: Integer; y: TMatrix) : TMatrix;

implementation
    {$I D:\Вадик\Лабы\2\1\MissingPartOfTheCode.Pas}
end.
