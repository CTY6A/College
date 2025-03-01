Program ArraySort;
//the program transforms the input array into an array in //which the elements stand by the increase in their
//occurrence in the array

{$APPTYPE CONSOLE}

Uses
  SysUtils;

Var
  B, NumOfEl, ResultArray: Array of Integer;
//B - input array
//NumOfEl - array with Num Of Elements
//ResultArrray - Result Array

  ElExistence: Array of Boolean;
//ElExistence - Element Existence

  n , i, j, IdOfMinNum, Indent: Integer;
//n - num of elements in arrays
//IdOfMinNum - Id Of Min Num
//Indent - Indent

Begin
//input and verification data 
  Repeat 
    Write ('enter the length of the array: ');
    Readln (n);

    {if data incorrect - show message about it}
    If (n <= 0) then
      Writeln ('incorrect input data. try now');
  Until (n > 0);

//initializing arrays for job
  SetLength(B,n);
  SetLength(NumOfEl,n);
  SetLength(ElExistence,n);
  SetLength(ResultArray,n);

//populating source array with random numbers
//and initializing other arrays
  For i:= 0 to n - 1 do
  Begin
    Readln(B[i]);
    NumOfEl[i] := 0;
    ElExistence[i] := True
  End;

//cycle for find num of the same elements for each element:
  {this cycle goes through every element that exists}
  For i:=0 to n - 1 do
    If ElExistence[i] then

//cycle are finding same elements, records their number in //a separate array (NumOfEl)
      For j := i  to n - 1 do
        If (B[i] = B[j]) then
        Begin
          NumOfEl[i] := NumOfEl[i] + 1;

          {assigns a repeating element to the status of non-existent}
          If NumOfEl[i] > 1 then
            ElExistence[j] := False
        End;

//initializing cycle parameters
  Indent := 0;

//cycle for permutation of elements in order of increasing
//their occurrence in the source array:
{array are scanned until the result array is filled}
  While (Indent <> n) do
    For j := 0 to n - 1 do

      {if the element exists - go next}
      If ElExistence[j] and (Indent <> n) then
      Begin

//cycle for find identification of minimum num in NumOfEl
        {initializing parameters}
        IdOfMinNum := j;
        For i := 0 to n-1 do
          If (NumOfEl[IdOfMinNum] > NumOfEl[i]) and ElExistence[i] then
            IdOfMinNum := i;

//cycle for records the result array
        For i := 0 to NumOfEl[IdOfMinNum] - 1 do
          ResultArray[i + Indent] := B[IdOfMinNum];

//cycle for giving non-existent status to repeated elements
        For i:= IdOfMinNum to n - 1 do
          If (B[IdOfMinNum] = B[i]) then
            ElExistence[i] := False ;

//increasing indent
        Indent := Indent + NumOfEl[IdOfMinNum];
      End;

//data output
  Writeln ('the result array: ');
  For i:= 0 to n - 1 do
     Write (ResultArray[i],' ');
  Readln
End. 

