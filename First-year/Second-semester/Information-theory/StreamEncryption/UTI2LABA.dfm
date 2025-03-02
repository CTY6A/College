object FMain: TFMain
  Left = 360
  Top = 251
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = 'Main'
  ClientHeight = 682
  ClientWidth = 976
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnActivate = FormActivate
  PixelsPerInch = 96
  TextHeight = 13
  object Lx1: TLabel
    Left = 102
    Top = 81
    Width = 12
    Height = 65
    Caption = 'X1'#13#10#13#10'X2'#13#10#13#10'X3'
    Visible = False
  end
  object L1: TLabel
    Left = 336
    Top = 80
    Width = 140
    Height = 13
    Caption = 'P(m)=x^26+x^8+x^7+x+1'
  end
  object L2: TLabel
    Left = 336
    Top = 80
    Width = 152
    Height = 13
    Caption = 'P(m)=x^34+x^15+x^14+x+1'
    Visible = False
  end
  object L3: TLabel
    Left = 336
    Top = 80
    Width = 140
    Height = 13
    Caption = 'P(m)=x^24+x^4+x^3+x+1'
    Visible = False
  end
  object L31: TLabel
    Left = 336
    Top = 106
    Width = 152
    Height = 13
    Caption = 'P(m)=x^34+x^15+x^14+x+1'
    Visible = False
  end
  object L21: TLabel
    Left = 336
    Top = 132
    Width = 140
    Height = 13
    Caption = 'P(m)=x^24+x^4+x^3+x+1'
    Visible = False
  end
  object CBEncryption: TComboBox
    Left = 120
    Top = 48
    Width = 145
    Height = 21
    ItemHeight = 13
    TabOrder = 0
    OnChange = CBEncryptionChange
    OnKeyPress = CBEncryptionKeyPress
    Items.Strings = (
      'LFSR1'
      'LFSR2'
      'LFSR3'
      #1043#1077#1092#1092#1077)
  end
  object EFirst: TEdit
    Left = 120
    Top = 80
    Width = 209
    Height = 21
    MaxLength = 26
    TabOrder = 1
    OnKeyPress = EFirstKeyPress
  end
  object ESecond: TEdit
    Left = 120
    Top = 104
    Width = 209
    Height = 21
    MaxLength = 34
    TabOrder = 2
    Visible = False
    OnKeyPress = EFirstKeyPress
  end
  object EThird: TEdit
    Left = 120
    Top = 128
    Width = 209
    Height = 21
    MaxLength = 24
    TabOrder = 3
    Visible = False
    OnKeyPress = EFirstKeyPress
  end
  object MSource: TMemo
    Left = 0
    Top = 240
    Width = 321
    Height = 345
    ReadOnly = True
    ScrollBars = ssVertical
    TabOrder = 4
  end
  object MKey: TMemo
    Left = 328
    Top = 240
    Width = 321
    Height = 345
    ReadOnly = True
    ScrollBars = ssVertical
    TabOrder = 5
  end
  object MResult: TMemo
    Left = 654
    Top = 240
    Width = 321
    Height = 345
    ReadOnly = True
    ScrollBars = ssVertical
    TabOrder = 6
  end
  object BOF: TButton
    Left = 90
    Top = 200
    Width = 140
    Height = 25
    Caption = #1042#1099#1073#1077#1088#1080#1090#1077' '#1087#1077#1088#1074#1099#1081' '#1092#1072#1081#1083
    TabOrder = 7
    OnClick = BOFClick
  end
  object BGK: TButton
    Left = 418
    Top = 200
    Width = 140
    Height = 25
    Caption = #1057#1075#1077#1085#1077#1088#1080#1088#1086#1074#1072#1090#1100' '#1082#1083#1102#1095
    Enabled = False
    TabOrder = 8
    OnClick = BGKClick
  end
  object BSF: TButton
    Left = 744
    Top = 200
    Width = 140
    Height = 25
    Caption = #1042#1099#1073#1077#1088#1080#1090#1077' '#1074#1090#1086#1088#1086#1081' '#1092#1072#1081#1083
    Enabled = False
    TabOrder = 9
    OnClick = BSFClick
  end
  object BStart: TButton
    Left = 418
    Top = 624
    Width = 140
    Height = 25
    Caption = #1053#1072#1095#1072#1090#1100
    Enabled = False
    TabOrder = 10
    OnClick = BStartClick
  end
  object DOMain: TOpenDialog
    Left = 576
    Top = 64
  end
  object DSMain: TSaveDialog
    Left = 640
    Top = 64
  end
end
