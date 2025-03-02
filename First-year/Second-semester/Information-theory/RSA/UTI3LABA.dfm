object FMain: TFMain
  Left = 850
  Top = 134
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = 'Main'
  ClientHeight = 505
  ClientWidth = 648
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object L3: TLabel
    Left = 8
    Top = 8
    Width = 6
    Height = 13
    Caption = 'p'
  end
  object L31: TLabel
    Left = 8
    Top = 42
    Width = 6
    Height = 13
    Caption = 'q'
  end
  object L21: TLabel
    Left = 120
    Top = 12
    Width = 6
    Height = 13
    Caption = 'b'
  end
  object EP: TEdit
    Left = 32
    Top = 8
    Width = 41
    Height = 21
    MaxLength = 26
    TabOrder = 0
    OnKeyPress = EPKeyPress
  end
  object EQ: TEdit
    Left = 32
    Top = 40
    Width = 41
    Height = 21
    MaxLength = 34
    TabOrder = 1
    OnKeyPress = EPKeyPress
  end
  object EB: TEdit
    Left = 144
    Top = 8
    Width = 41
    Height = 21
    MaxLength = 24
    TabOrder = 2
    OnKeyPress = EPKeyPress
  end
  object MSource: TMemo
    Left = 0
    Top = 72
    Width = 321
    Height = 345
    ReadOnly = True
    ScrollBars = ssVertical
    TabOrder = 3
  end
  object MResult: TMemo
    Left = 326
    Top = 72
    Width = 321
    Height = 345
    ReadOnly = True
    ScrollBars = ssVertical
    TabOrder = 4
  end
  object BOF: TButton
    Left = 90
    Top = 32
    Width = 140
    Height = 25
    Caption = #1042#1099#1073#1077#1088#1080#1090#1077' '#1087#1077#1088#1074#1099#1081' '#1092#1072#1081#1083
    TabOrder = 5
    OnClick = BOFClick
  end
  object BSF: TButton
    Left = 416
    Top = 32
    Width = 140
    Height = 25
    Caption = #1042#1099#1073#1077#1088#1080#1090#1077' '#1074#1090#1086#1088#1086#1081' '#1092#1072#1081#1083
    Enabled = False
    TabOrder = 6
    OnClick = BSFClick
  end
  object BStart: TButton
    Left = 251
    Top = 448
    Width = 140
    Height = 25
    Caption = #1053#1072#1095#1072#1090#1100
    Enabled = False
    TabOrder = 7
    OnClick = BStartClick
  end
  object RBC: TRadioButton
    Left = 264
    Top = 8
    Width = 113
    Height = 17
    Caption = #1096#1080#1092#1088#1086#1074#1072#1085#1080#1077
    Checked = True
    TabOrder = 8
    TabStop = True
  end
  object RBDC: TRadioButton
    Left = 264
    Top = 40
    Width = 113
    Height = 17
    Caption = #1076#1077#1096#1080#1092#1088#1086#1074#1072#1085#1080#1077
    TabOrder = 9
  end
  object DOMain: TOpenDialog
    Left = 416
  end
  object DSMain: TSaveDialog
    Left = 440
  end
end
