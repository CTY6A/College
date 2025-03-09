--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   16:08:11 11/27/2020
-- Design Name:   
-- Module Name:   C:/Users/User/Desktop/LabsPOCP/Lab4/Signature_TB.vhd
-- Project Name:  Lab4
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: Signature
-- 
-- Dependencies:
-- 
-- Revision:
-- Revision 0.01 - File Created
-- Additional Comments:
--
-- Notes: 
-- This testbench has been automatically generated using types std_logic and
-- std_logic_vector for the ports of the unit under test.  Xilinx recommends
-- that these types always be used for the top-level I/O of a design in order
-- to guarantee that the testbench will bind correctly to the post-implementation 
-- simulation model.
--------------------------------------------------------------------------------
LIBRARY ieee;
USE ieee.std_logic_1164.ALL;
 
-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--USE ieee.numeric_std.ALL;
 
ENTITY Signature_TB IS
END Signature_TB;
 
ARCHITECTURE behavior OF Signature_TB IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT Signature
    PORT(
         CLK : IN  std_logic;
         RST : IN  std_logic;
         Pin : IN  std_logic;
         Pout : OUT  std_logic_vector(0 to 3)
        );
    END COMPONENT;
    

   --Inputs
   signal CLK : std_logic := '0';
   signal RST : std_logic := '0';
   signal Pin : std_logic := '0';

 	--Outputs
   signal Pout : std_logic_vector(0 to 3);
 
	constant CLK_Period: time := 10 ns;
BEGIN
 
	-- Instantiate the Unit Under Test (UUT) 
   uut: Signature PORT MAP (
          CLK => CLK,
          RST => RST,
          Pin => Pin,
          Pout => Pout
        ); 
		  
	-- Clock process definitions
	CLK_process :process
	begin
		CLK <= '0';
		wait for CLK_Period/2;
		CLK <= '1';
		wait for CLK_Period/2;
	end process;

   -- Stimulus process
   stim_proc: process
   begin	
		wait for CLK_Period;
		
		RST <= '0'; wait for CLK_Period;
		RST <= '1'; wait for 2*CLK_Period;
		RST <= '0';
		
		PIn <= '1'; wait for 8*CLK_Period;
		
		wait;
   end process;

END;
