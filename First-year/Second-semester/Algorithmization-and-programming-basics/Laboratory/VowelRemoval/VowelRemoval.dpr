program VowelRemoval;

{$APPTYPE CONSOLE}

uses
  SysUtils;

const
  Consonants: Set of 'A'..'z' = ['b'..'d', 'f', 'h', 'j'..'n', 'p'..'t', 'v'..'z'];
  Alphabet: String = 'abcdefghijklmnopqrstuvwxyz';

procedure Words(Str: String);

  procedure Output(Word: String);

  var
    i, j: Integer;

  begin
    for i := 1 to 26 do
      for j := 1 to Length (Word) do
        if (Word[j] = Alphabet[i]) and (Word[j] in Consonants) then
          write (Word[j], ' ')
  end;

var
  Word: String;
  i, j: Integer;

begin
  i := 1;
  j := 2;
  repeat
    while not ((Str[j] = ',') or (Str[j] = '.')) do
      inc(j);
    Word := Copy(Str, i, j - i);
    Output(Word);
    writeln;
    if Str[j] = ',' then
    begin
      i := j + 1;
      j := j + 2;
    end
  until Str[j] = '.'
end;

var
  StrOfWords: String;
  i: Integer;

begin
  StrOfWords := 'write,pen,xlkipu.';
  Words(StrOfWords);
  readln
end.
