program ListOfListsParser;

{$APPTYPE CONSOLE}

uses
  SysUtils;

function ListOfLists(List: String): Boolean;
begin
  Result := False;
  if Length(List) > 0 then
    if List[1] in ['A'..'z'] then
    begin
      Delete (List, 1, 1);
      if Length(List) > 0 then
        case List[1] of
          'A'..'z' : if ListOfLists(List) then
                       Result := True;
          ',', ';' : begin
                       Delete (List, 1, 1);
                       if ListOfLists(List) then
                         Result := True
                     end;
          '.' : begin
                  Delete (List, 1, 1);
                  if Length(List) = 0 then
                    Result := True
                end
        end
    end
end;

var
  SourceList: String;

begin
  readln (SourceList);
  writeln (ListOfLists(SourceList));
  readln
end.
 