unit U3;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, Grids, StdCtrls;

type
  SetKit = Set of Char;

  TFMain = class(TForm)
    BMain: TButton;
    DOMain: TOpenDialog;
    SGChep: TStringGrid;
    SGSpen: TStringGrid;
    L1: TLabel;
    L2: TLabel;
    SG1: TStringGrid;
    procedure BMainClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);

  private
    procedure Heart(var F: Text);
    procedure WordCreate (var Str, Word: String; Kit: SetKit);
    procedure InputOfTheWord (var Word: string; Table: TStringGrid);
  public
    { Public declarations }

  end;

var
  FMain: TFMain;

  F: Text;

  Str, Word: String;
  C, Ib, IO : Boolean;
  Arr: array of record
                  Name: string;
                  Num: Integer;
                  C, I, IO: Boolean;
                end;

  q: record
       P, M, C, T: Integer
     end;

  i, bort, Pi, Mi, Ci, Ti: Integer;

implementation

{$R *.dfm}

procedure TFMain.InputOfTheWord(var Word: string; Table: TStringGrid);
begin
  i:=2;
  while (Word <> Table.Cells[1,i-1]) and (i<Table.RowCount) do
    Inc(i);
  if i<Table.RowCount then
    Table.Cells[2,i-1] := IntToStr(StrToInt(Table.Cells[2,i-1]) + 1)
  else
  begin
    Table.Cells[0,i-1]:=IntToStr(i-1);
    Table.Cells[1,i-1]:=Word;
    Table.Cells[2,i-1] := '1';
    Table.RowCount:=i+1;
  end;

end;

procedure TFMain.Heart(var F: Text);
begin
  Reset(F);

//initialization
  C := False;
  Ib := False;
  IO := False;

  while not Eof(F) do
  begin
    Readln(F,Str);

    while Length(Str) > 0 do
    case Str[1] of
      'A'..'Z',
      'a'..'z', '_' : begin
                        WordCreate(Str,Word,['A'..'Z','a'..'z','_','0'..'9']);
                        Delete(Str,1,Length(Word));

                        if (Word = 'while') or (Word = 'for') or (Word = 'if') or (Word = 'case') or (Word = 'elseif') then
                        begin
                            Delete(Str,1,Pos('(',Str));
                            bort := 1;

                            C := True;
                        end;
                        if (Word = 'readline') or (Word = 'read')or (Word = 'readln') then
                        begin
                            Delete(Str,1,Pos('(',Str));
                            bort := 1;

                            Ib := True;
                        end;
                        if (Word = 'echo') then
                          IO := True
                      end;

      ';'           : begin Delete(Str,1,1); if not IO then IO := False end;

      '('           : begin Delete(Str,1,1); if bort > 0 then Inc(bort) end;

      ')'           : begin
                        Delete(Str,1,1);
                        if bort > 0 then
                          Dec(bort);

                        if (C or Ib) and (bort = 0) then
                        begin
                          C := False;
                          Ib := False
                        end
                      end;

      '$'           : begin
                        Delete(Str,1,1);
                        WordCreate(Str,Word,['A'..'Z','a'..'z','_','0'..'9']);
                        Delete(Str,1,Length(Word));

                        for i := 0 to Length(Arr) - 1 do
                          if Word = Arr[i].Name then
                          begin
                            Word := '';
                            Inc(Arr[i].Num);

                            if IO or Ib then
                              Arr[i].IO := True;

                            if bort > 0 then
                              if not Arr[i].C and C then
                                Arr[i].C := True
                              else
                                if not Arr[i].I and Ib then
                                  Arr[i].I := True
                          end;

                        if Word <> '' then
                        begin
                          SetLength(Arr, Length(Arr) + 1);
                          Arr[Length(Arr) - 1].Name := Word;
                          Arr[Length(Arr) - 1].Num := 1;

                          if (IO or Ib) then
                            Arr[Length(Arr) - 1].IO := True
                          else
                            Arr[Length(Arr) - 1].IO := False;

                          if (bort > 0) then
                            if C then
                            begin
                              Arr[Length(Arr) - 1].C := True;
                              Arr[Length(Arr) - 1].I := False
                            end
                            else
                              if Ib then
                              begin
                                Arr[Length(Arr) - 1].C := False;
                                Arr[Length(Arr) - 1].I := True
                              end
                              else
                          else
                          begin
                              Arr[Length(Arr) - 1].C := False;
                              Arr[Length(Arr) - 1].I := False
                          end
                        end
                      end;

    else Delete(Str,1,1)
    end
  end;

  Pi := 1;
  Mi := 1;
  Ci := 1;
  Ti := 1;
  bort := 1;

  for i := 0 to Length(Arr) - 1 do
    if Arr[i].Num > 1 then
    begin
      if bort = SGSpen.ColCount then
        SGSpen.ColCount := SGSpen.ColCount + 1;
      SGSpen.Cells[bort, 0] := Arr[i].Name;
      SGSpen.Cells[bort, 1] := IntToStr(Arr[i].Num - 1);
      Inc(bort);

      if Arr[i].C then
      begin
        if Ci = SGChep.RowCount then
          SGChep.RowCount := SGChep.RowCount + 1;
        SGChep.Cells[3,Ci] := Arr[i].Name;
        Inc(Ci)
      end
      else
        if Arr[i].I then
        begin
          if Pi = SGChep.RowCount then
            SGChep.RowCount := SGChep.RowCount + 1;;
          SGChep.Cells[1,Pi] := Arr[i].Name;
          Inc(Pi)
        end
        else
        begin
          if Mi = SGChep.RowCount then
            SGChep.RowCount := SGChep.RowCount + 1;
          SGChep.Cells[2,Mi] := Arr[i].Name;
          Inc(Mi)
        end
    end
    else
      begin
        if Ti = SGChep.RowCount then
          SGChep.RowCount := SGChep.RowCount + 1;
        SGChep.Cells[4,Ti] := Arr[i].Name;
        Inc(Ti)
      end;

  SGSpen.ColCount := SGSpen.ColCount + 1;
  SGSpen.Cells[SGSpen.ColCount - 1, 1] := '0';
  for i := 1 to SGSpen.ColCount - 2 do
    SGSpen.Cells[SGSpen.ColCount - 1, 1] := IntToStr(StrToInt(SGSpen.Cells[SGSpen.ColCount - 1, 1]) + StrToInt(SGSpen.Cells[i, 1]));

  SGSpen.Cells[0,0] := 'Идентификатор';
  SGSpen.Cells[0,1] := 'Спен';
  SGSpen.Cells[SGSpen.ColCount - 1, 0] := 'Суммарный спен программы';

  SGChep.Cells[1,0] := 'P';
  SGChep.Cells[2,0] := 'M';
  SGChep.Cells[3,0] := 'C';
  SGChep.Cells[4,0] := 'T';

  SGChep.Cells[5,0] := 'P';
  SGChep.Cells[6,0] := 'M';
  SGChep.Cells[7,0] := 'C';
  SGChep.Cells[8,0] := 'T';

  SGChep.Cells[0,1] := 'Группа переменных';
  SGChep.Cells[0,2] := 'Переменные, относящиеся к группе';

  with q do
  begin
    P := Pi - 1;
    M := Mi - 1;
    C := Ci - 1;
    T := Ti - 1
  end;

  Pi := 1;
  Mi := 1;
  Ci := 1;
  Ti := 1;
  bort := 1;

  for i := 0 to Length(Arr) - 1 do
    if Arr[i].IO then
      if Arr[i].Num > 1 then
      begin
        if Arr[i].C then
        begin
          if Ci = SGChep.RowCount then
            SGChep.RowCount := SGChep.RowCount + 1;
          SGChep.Cells[7,Ci] := Arr[i].Name;
          Inc(Ci)
        end
        else
          if Arr[i].I then
          begin
            if Pi = SGChep.RowCount then
              SGChep.RowCount := SGChep.RowCount + 1;;
            SGChep.Cells[5,Pi] := Arr[i].Name;
            Inc(Pi)
          end
          else
          begin
            if Mi = SGChep.RowCount then
              SGChep.RowCount := SGChep.RowCount + 1;
            SGChep.Cells[6,Mi] := Arr[i].Name;
            Inc(Mi)
          end
      end
      else
        begin
          if Ti = SGChep.RowCount then
            SGChep.RowCount := SGChep.RowCount + 1;
          SGChep.Cells[8,Ti] := Arr[i].Name;
          Inc(Ti)
        end;

  SGChep.RowCount := SGChep.RowCount + 1;
  SGChep.Cells[0,SGChep.RowCount - 1] := 'Количество переменных в группе';

  SGChep.Cells[1,SGChep.RowCount - 1] := IntToStr(q.P);
  SGChep.Cells[2,SGChep.RowCount - 1] := IntToStr(q.M);
  SGChep.Cells[3,SGChep.RowCount - 1] := IntToStr(q.C);
  SGChep.Cells[4,SGChep.RowCount - 1] := IntToStr(q.T);

  SGChep.Cells[5,SGChep.RowCount - 1] := IntToStr(Pi - 1);
  SGChep.Cells[6,SGChep.RowCount - 1] := IntToStr(Mi - 1);
  SGChep.Cells[7,SGChep.RowCount - 1] := IntToStr(Ci - 1);
  SGChep.Cells[8,SGChep.RowCount - 1] := IntToStr(Ti - 1);

  SG1.Cells[0,0] := 'Метрика Чепина';
  with q do
    SG1.Cells[1,0] := FloatToStr(P + M * 2 + C * 3 + T * 0.5);
  SG1.Cells[2,0] := FloatToStr(Pi - 1 + (Mi - 1) * 2 + (Ci - 1) * 3 + (Ti - 1) * 0.5);

  CloseFile(F);

  SGSpen.Visible := True;
  SGChep.Visible := True;
  L1.Visible := True;
  L2.Visible := True;
  SG1.Visible := True
end;

procedure TFMain.WordCreate (var Str, Word: String; Kit: SetKit);
begin
  i := 1;
  while Str[i] in Kit do
    inc(i);
  Word := Copy(Str,1,i-1);
end;

procedure TFMain.BMainClick(Sender: TObject);
begin
  if DOMain.Execute then
  begin
    AssignFile(F,DOMain.FileName);

    for i := 0 to 8 do
      SGChep.Cols[i].Clear;
    SGChep.RowCount := 2;

    for i := 0 to 1 do
      SGSpen.Rows[i].Clear;
    SGSpen.ColCount := 2;

    SetLength(Arr,0);
    Heart(F)
  end
end;

procedure TFMain.FormActivate(Sender: TObject);
begin
  bort := 0;
end;

end.
