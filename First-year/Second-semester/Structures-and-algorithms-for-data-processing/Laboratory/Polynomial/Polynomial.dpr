program Polynomial;

{$APPTYPE CONSOLE}

uses
  SysUtils;

type
  Multi=^elem;
  elem = record
            n:Integer;
            a:Integer;
            Next:Multi;
         end;
var
  p,q,r:Multi;
  x:integer;


function ReadPoly(): Multi;
var
  ListHead,x:Multi;
  i,a,n:integer;
begin
  write('Enter the power of first polynomial: ');
  readln(n);
  write('Enter the coefficients: ');

  ListHead:=nil;
  x:=nil;
  for i := 0 to n do
  begin
    readln(a);
    if a<>0 then
    begin
      if ListHead=nil then
      begin
        new(x);
        x^.n:=n;
        x^.a:=a;
        x^.next:=nil;
        ListHead:=x;
      end
      else
      begin
        new(x^.Next);
        x:=x^.Next;
        x^.n:=n;
        x^.a:=a;
        x^.Next:=nil;
      end;
    end;
    Dec(n);
  end;  

  Result:=ListHead;
end;


procedure WritePoly(T:Multi);
begin
  while T <> nil do
  begin
    if T^.n>1 then
      write(IntToStr(T^.a)+'x^'+IntToStr(T^.n));
    if T^.n=1 then
      write(IntToStr(T^.a)+'x');
    if T^.n=0 then
      write(IntToStr(T^.a));

    if T.Next <> nil then
      write(' + ');

    T:=T^.Next
  end;
  writeln;
end;

function Equality(p,q:Multi):boolean;
begin
  Result:=true;
  while (p<>nil) and (q<>nil) and Result do
  begin
    if (p^.n <> q^.n) or (p^.a <> q^.a) then
      Result:=false;
    p:=p^.Next;
    q:=q^.Next;
  end;
end;

function Meaning(p:Multi;x:integer):integer;
begin
  Result:=0;
  while p <> nil do
  begin
    if x > 0 then
      Result:=Result + p^.a * round(exp(p^.n * ln(x)))
    else
      if (x = 0) and (p^.n = 0) then
        Result:=p^.a;

    p:=p^.Next
  end
end;

procedure Add (p,q:Multi; out r:Multi);
var
  T:Multi;
  fl:boolean;
begin
  if (p <> nil) or (q <> nil) then
  begin
    new(T);
    r:=T;
    T^.Next:=nil;
    while (p <> nil) and (q <> nil) do
    begin
      if q^.n > p^.n then
      begin
        T^.n := q^.n;
        T^.a := q^.a;

        q:=q^.Next
      end
      else
        if q^.n < p^.n then
        begin
          T^.n := p^.n;
          T^.a := p^.a;

          p:=p^.Next
        end
        else
          if q^.n = p^.n then
          begin
            T^.n := q^.n;
            T^.a := p^.a + q^.a;

            q:=q^.Next;
            p:=p^.Next
          end;

      if (p <> nil) or (q <> nil) then
      begin
        New(T^.Next);
        T:=T^.Next;
        T^.Next := nil
      end
    end;

    while q <> nil do
    begin
      T^.n := q^.n;
      T^.a := q^.a;

      q:=q^.Next;

      if q <> nil then
      begin
        New(T^.Next);
        T:=T^.Next;
        T^.Next := nil
      end
    end;

    while p <> nil do
    begin
      T^.n := p^.n;
      T^.a := p^.a;

      p:=p^.Next;

      if p <> nil then
      begin
        New(T^.Next);
        T:=T^.Next;
        T^.Next := nil
      end
    end
  end
  else
    r:=nil
end;


procedure Delete(T:Multi);
var
  ListHead:Multi;
begin
  while T <> nil do
  begin
    ListHead := T^.Next;
    Dispose(T);
    T := ListHead
  end
end;

begin
  p:=ReadPoly;
  write('Your 1 polynomial: ');
  WritePoly(p);
  writeln;

  q:=ReadPoly;
  write('Your 2 polynomial: ');
  WritePoly(q);
  writeln;
  writeln;


  if Equality(p,q) then
    writeln('Polinomilas are equal')
  else
    writeln('Polinomilas are not equal');
  writeln;

  write('Enter x for the first polynomial: ');
  readln(x);
  writeln('The value of the first polynomial is ',Meaning(p,x));
  writeln;

  Add(p,q,r);
  write('Polynomial sum: ');
  WritePoly(r);

  Delete(p);
  Delete(q);
  Delete(r);
  readln;
end.
