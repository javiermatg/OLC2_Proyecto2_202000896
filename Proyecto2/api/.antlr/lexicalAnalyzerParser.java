// Generated from /home/snier/Documents/LabCompi2/compi2Damian/TestEnsamblador/Proyecto1/api/lexicalAnalyzer.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue"})
public class lexicalAnalyzerParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, T__33=34, T__34=35, T__35=36, T__36=37, T__37=38, 
		T__38=39, T__39=40, T__40=41, T__41=42, T__42=43, T__43=44, T__44=45, 
		T__45=46, T__46=47, T__47=48, T__48=49, LINE_COMMENT=50, MULTILINE_COMMENT=51, 
		NEWLINE=52, NULL=53, BOOL=54, ID=55, INT=56, DECIMAL=57, CHARACTER=58, 
		STRING=59;
	public static final int
		RULE_init = 0, RULE_instruction = 1, RULE_block = 2, RULE_funcInstructions = 3, 
		RULE_funcParams = 4, RULE_func = 5, RULE_pars = 6, RULE_stmtSlice = 7, 
		RULE_stmtVariables = 8, RULE_stmtAssign = 9, RULE_print = 10, RULE_instructionIf = 11, 
		RULE_forInstruction = 12, RULE_forDeclare = 13, RULE_switchInstruction = 14, 
		RULE_cases = 15, RULE_breakInstruction = 16, RULE_continueInstruction = 17, 
		RULE_returnFunc = 18, RULE_expression = 19, RULE_type = 20, RULE_expr = 21;
	private static String[] makeRuleNames() {
		return new String[] {
			"init", "instruction", "block", "funcInstructions", "funcParams", "func", 
			"pars", "stmtSlice", "stmtVariables", "stmtAssign", "print", "instructionIf", 
			"forInstruction", "forDeclare", "switchInstruction", "cases", "breakInstruction", 
			"continueInstruction", "returnFunc", "expression", "type", "expr"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "';'", "'{'", "'}'", "'func'", "'('", "')'", "':'", "','", "'var'", 
			"'[]'", "'='", "':='", "'fmt.Println'", "'if'", "'else if'", "'else'", 
			"'for'", "'switch'", "'default'", "'case'", "'break'", "'continue'", 
			"'return'", "'int'", "'float64'", "'string'", "'bool'", "'rune'", "'-'", 
			"'*'", "'/'", "'+'", "'%'", "'!'", "'strconv.Atoi'", "'strconv.ParseFloat'", 
			"'reflect.TypeOf'", "'=='", "'!='", "'>='", "'>'", "'<='", "'<'", "'&&'", 
			"'||'", "'++'", "'--'", "'+='", "'-='", null, null, null, "'nil'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, "LINE_COMMENT", "MULTILINE_COMMENT", "NEWLINE", "NULL", "BOOL", 
			"ID", "INT", "DECIMAL", "CHARACTER", "STRING"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "lexicalAnalyzer.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public lexicalAnalyzerParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@SuppressWarnings("CheckReturnValue")
	public static class InitContext extends ParserRuleContext {
		public List<InstructionContext> instruction() {
			return getRuleContexts(InstructionContext.class);
		}
		public InstructionContext instruction(int i) {
			return getRuleContext(InstructionContext.class,i);
		}
		public InitContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_init; }
	}

	public final InitContext init() throws RecognitionException {
		InitContext _localctx = new InitContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_init);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(47);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & 1143914563602113076L) != 0)) {
				{
				{
				setState(44);
				instruction();
				}
				}
				setState(49);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class InstructionContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public StmtVariablesContext stmtVariables() {
			return getRuleContext(StmtVariablesContext.class,0);
		}
		public StmtAssignContext stmtAssign() {
			return getRuleContext(StmtAssignContext.class,0);
		}
		public FuncInstructionsContext funcInstructions() {
			return getRuleContext(FuncInstructionsContext.class,0);
		}
		public PrintContext print() {
			return getRuleContext(PrintContext.class,0);
		}
		public InstructionIfContext instructionIf() {
			return getRuleContext(InstructionIfContext.class,0);
		}
		public StmtSliceContext stmtSlice() {
			return getRuleContext(StmtSliceContext.class,0);
		}
		public ForInstructionContext forInstruction() {
			return getRuleContext(ForInstructionContext.class,0);
		}
		public SwitchInstructionContext switchInstruction() {
			return getRuleContext(SwitchInstructionContext.class,0);
		}
		public BreakInstructionContext breakInstruction() {
			return getRuleContext(BreakInstructionContext.class,0);
		}
		public ContinueInstructionContext continueInstruction() {
			return getRuleContext(ContinueInstructionContext.class,0);
		}
		public ReturnFuncContext returnFunc() {
			return getRuleContext(ReturnFuncContext.class,0);
		}
		public BlockContext block() {
			return getRuleContext(BlockContext.class,0);
		}
		public InstructionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_instruction; }
	}

	public final InstructionContext instruction() throws RecognitionException {
		InstructionContext _localctx = new InstructionContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_instruction);
		int _la;
		try {
			setState(102);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,14,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(50);
				expression();
				setState(52);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(51);
					match(T__0);
					}
				}

				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(54);
				stmtVariables();
				setState(56);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(55);
					match(T__0);
					}
				}

				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(58);
				stmtAssign();
				setState(60);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(59);
					match(T__0);
					}
				}

				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(62);
				funcInstructions();
				setState(64);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(63);
					match(T__0);
					}
				}

				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(66);
				print();
				setState(68);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(67);
					match(T__0);
					}
				}

				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(70);
				instructionIf();
				setState(72);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(71);
					match(T__0);
					}
				}

				}
				break;
			case 7:
				enterOuterAlt(_localctx, 7);
				{
				setState(74);
				stmtSlice();
				setState(76);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(75);
					match(T__0);
					}
				}

				}
				break;
			case 8:
				enterOuterAlt(_localctx, 8);
				{
				setState(78);
				forInstruction();
				setState(80);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(79);
					match(T__0);
					}
				}

				}
				break;
			case 9:
				enterOuterAlt(_localctx, 9);
				{
				setState(82);
				switchInstruction();
				setState(84);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(83);
					match(T__0);
					}
				}

				}
				break;
			case 10:
				enterOuterAlt(_localctx, 10);
				{
				setState(86);
				breakInstruction();
				setState(88);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(87);
					match(T__0);
					}
				}

				}
				break;
			case 11:
				enterOuterAlt(_localctx, 11);
				{
				setState(90);
				continueInstruction();
				setState(92);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(91);
					match(T__0);
					}
				}

				}
				break;
			case 12:
				enterOuterAlt(_localctx, 12);
				{
				setState(94);
				returnFunc();
				setState(96);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(95);
					match(T__0);
					}
				}

				}
				break;
			case 13:
				enterOuterAlt(_localctx, 13);
				{
				setState(98);
				block();
				setState(100);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(99);
					match(T__0);
					}
				}

				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class BlockContext extends ParserRuleContext {
		public BlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_block; }
	 
		public BlockContext() { }
		public void copyFrom(BlockContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class BlockStmtContext extends BlockContext {
		public List<InstructionContext> instruction() {
			return getRuleContexts(InstructionContext.class);
		}
		public InstructionContext instruction(int i) {
			return getRuleContext(InstructionContext.class,i);
		}
		public BlockStmtContext(BlockContext ctx) { copyFrom(ctx); }
	}

	public final BlockContext block() throws RecognitionException {
		BlockContext _localctx = new BlockContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_block);
		int _la;
		try {
			_localctx = new BlockStmtContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(104);
			match(T__1);
			setState(108);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & 1143914563602113076L) != 0)) {
				{
				{
				setState(105);
				instruction();
				}
				}
				setState(110);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(111);
			match(T__2);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class FuncInstructionsContext extends ParserRuleContext {
		public FuncInstructionsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_funcInstructions; }
	 
		public FuncInstructionsContext() { }
		public void copyFrom(FuncInstructionsContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class FuncStmtContext extends FuncInstructionsContext {
		public TypeContext returnT;
		public TerminalNode ID() { return getToken(lexicalAnalyzerParser.ID, 0); }
		public InstructionContext instruction() {
			return getRuleContext(InstructionContext.class,0);
		}
		public FuncParamsContext funcParams() {
			return getRuleContext(FuncParamsContext.class,0);
		}
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public FuncStmtContext(FuncInstructionsContext ctx) { copyFrom(ctx); }
	}

	public final FuncInstructionsContext funcInstructions() throws RecognitionException {
		FuncInstructionsContext _localctx = new FuncInstructionsContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_funcInstructions);
		int _la;
		try {
			_localctx = new FuncStmtContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(113);
			match(T__3);
			setState(114);
			match(ID);
			setState(115);
			match(T__4);
			setState(117);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==ID) {
				{
				setState(116);
				funcParams();
				}
			}

			setState(119);
			match(T__5);
			setState(120);
			match(T__6);
			setState(122);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & 520093696L) != 0)) {
				{
				setState(121);
				((FuncStmtContext)_localctx).returnT = type();
				}
			}

			setState(124);
			match(T__1);
			setState(125);
			instruction();
			setState(126);
			match(T__2);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class FuncParamsContext extends ParserRuleContext {
		public List<TerminalNode> ID() { return getTokens(lexicalAnalyzerParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(lexicalAnalyzerParser.ID, i);
		}
		public List<TypeContext> type() {
			return getRuleContexts(TypeContext.class);
		}
		public TypeContext type(int i) {
			return getRuleContext(TypeContext.class,i);
		}
		public FuncParamsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_funcParams; }
	}

	public final FuncParamsContext funcParams() throws RecognitionException {
		FuncParamsContext _localctx = new FuncParamsContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_funcParams);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(128);
			match(ID);
			setState(129);
			type();
			setState(135);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__7) {
				{
				{
				setState(130);
				match(T__7);
				setState(131);
				match(ID);
				setState(132);
				type();
				}
				}
				setState(137);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class FuncContext extends ParserRuleContext {
		public ParsContext pars() {
			return getRuleContext(ParsContext.class,0);
		}
		public FuncContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_func; }
	}

	public final FuncContext func() throws RecognitionException {
		FuncContext _localctx = new FuncContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_func);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(138);
			match(T__4);
			setState(140);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & 1143914563587014688L) != 0)) {
				{
				setState(139);
				pars();
				}
			}

			setState(142);
			match(T__5);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ParsContext extends ParserRuleContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public ParsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pars; }
	}

	public final ParsContext pars() throws RecognitionException {
		ParsContext _localctx = new ParsContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_pars);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(144);
			expr(0);
			setState(149);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__7) {
				{
				{
				setState(145);
				match(T__7);
				setState(146);
				expr(0);
				}
				}
				setState(151);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StmtSliceContext extends ParserRuleContext {
		public StmtSliceContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_stmtSlice; }
	 
		public StmtSliceContext() { }
		public void copyFrom(StmtSliceContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class StmtSlicesContext extends StmtSliceContext {
		public TerminalNode ID() { return getToken(lexicalAnalyzerParser.ID, 0); }
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public StmtSlicesContext(StmtSliceContext ctx) { copyFrom(ctx); }
	}

	public final StmtSliceContext stmtSlice() throws RecognitionException {
		StmtSliceContext _localctx = new StmtSliceContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_stmtSlice);
		int _la;
		try {
			_localctx = new StmtSlicesContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(152);
			match(T__8);
			setState(153);
			match(ID);
			setState(154);
			match(T__9);
			setState(155);
			type();
			setState(166);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__10) {
				{
				setState(156);
				match(T__10);
				setState(157);
				match(T__1);
				setState(162);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & 1143914563587014944L) != 0)) {
					{
					setState(160);
					_errHandler.sync(this);
					switch (_input.LA(1)) {
					case T__4:
					case T__28:
					case T__33:
					case T__34:
					case T__35:
					case T__36:
					case NULL:
					case BOOL:
					case ID:
					case INT:
					case DECIMAL:
					case CHARACTER:
					case STRING:
						{
						setState(158);
						expr(0);
						}
						break;
					case T__7:
						{
						setState(159);
						match(T__7);
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					}
					setState(164);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(165);
				match(T__2);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StmtVariablesContext extends ParserRuleContext {
		public StmtVariablesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_stmtVariables; }
	 
		public StmtVariablesContext() { }
		public void copyFrom(StmtVariablesContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class StmtVarContext extends StmtVariablesContext {
		public TerminalNode ID() { return getToken(lexicalAnalyzerParser.ID, 0); }
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public StmtVarContext(StmtVariablesContext ctx) { copyFrom(ctx); }
	}

	public final StmtVariablesContext stmtVariables() throws RecognitionException {
		StmtVariablesContext _localctx = new StmtVariablesContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_stmtVariables);
		int _la;
		try {
			_localctx = new StmtVarContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(168);
			match(T__8);
			setState(169);
			match(ID);
			setState(170);
			type();
			setState(173);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__10) {
				{
				setState(171);
				match(T__10);
				setState(172);
				expr(0);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StmtAssignContext extends ParserRuleContext {
		public StmtAssignContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_stmtAssign; }
	 
		public StmtAssignContext() { }
		public void copyFrom(StmtAssignContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class StmtVarAssignContext extends StmtAssignContext {
		public TerminalNode ID() { return getToken(lexicalAnalyzerParser.ID, 0); }
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public StmtVarAssignContext(StmtAssignContext ctx) { copyFrom(ctx); }
	}

	public final StmtAssignContext stmtAssign() throws RecognitionException {
		StmtAssignContext _localctx = new StmtAssignContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_stmtAssign);
		try {
			_localctx = new StmtVarAssignContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(175);
			match(ID);
			setState(176);
			match(T__11);
			setState(177);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class PrintContext extends ParserRuleContext {
		public PrintContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_print; }
	 
		public PrintContext() { }
		public void copyFrom(PrintContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class PrintVarContext extends PrintContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public PrintVarContext(PrintContext ctx) { copyFrom(ctx); }
	}

	public final PrintContext print() throws RecognitionException {
		PrintContext _localctx = new PrintContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_print);
		int _la;
		try {
			_localctx = new PrintVarContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(179);
			match(T__12);
			setState(180);
			match(T__4);
			setState(181);
			expr(0);
			setState(186);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__7) {
				{
				{
				setState(182);
				match(T__7);
				setState(183);
				expr(0);
				}
				}
				setState(188);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(189);
			match(T__5);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class InstructionIfContext extends ParserRuleContext {
		public InstructionIfContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_instructionIf; }
	 
		public InstructionIfContext() { }
		public void copyFrom(InstructionIfContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class IfStmtContext extends InstructionIfContext {
		public ExprContext exprIf;
		public InstructionContext lsIf;
		public ExprContext exprElseIf;
		public InstructionContext lsElseIf;
		public InstructionContext lsElse;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public List<InstructionContext> instruction() {
			return getRuleContexts(InstructionContext.class);
		}
		public InstructionContext instruction(int i) {
			return getRuleContext(InstructionContext.class,i);
		}
		public IfStmtContext(InstructionIfContext ctx) { copyFrom(ctx); }
	}

	public final InstructionIfContext instructionIf() throws RecognitionException {
		InstructionIfContext _localctx = new InstructionIfContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_instructionIf);
		int _la;
		try {
			_localctx = new IfStmtContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(191);
			match(T__13);
			setState(192);
			((IfStmtContext)_localctx).exprIf = expr(0);
			setState(193);
			match(T__1);
			setState(194);
			((IfStmtContext)_localctx).lsIf = instruction();
			setState(195);
			match(T__2);
			setState(204);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__14) {
				{
				{
				{
				setState(196);
				match(T__14);
				setState(197);
				((IfStmtContext)_localctx).exprElseIf = expr(0);
				setState(198);
				match(T__1);
				setState(199);
				((IfStmtContext)_localctx).lsElseIf = instruction();
				setState(200);
				match(T__2);
				}
				}
				}
				setState(206);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(212);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__15) {
				{
				setState(207);
				match(T__15);
				setState(208);
				match(T__1);
				setState(209);
				((IfStmtContext)_localctx).lsElse = instruction();
				setState(210);
				match(T__2);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ForInstructionContext extends ParserRuleContext {
		public ForInstructionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_forInstruction; }
	 
		public ForInstructionContext() { }
		public void copyFrom(ForInstructionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ForStmtContext extends ForInstructionContext {
		public InstructionContext lsfor;
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public InstructionContext instruction() {
			return getRuleContext(InstructionContext.class,0);
		}
		public List<ForDeclareContext> forDeclare() {
			return getRuleContexts(ForDeclareContext.class);
		}
		public ForDeclareContext forDeclare(int i) {
			return getRuleContext(ForDeclareContext.class,i);
		}
		public ForStmtContext(ForInstructionContext ctx) { copyFrom(ctx); }
	}

	public final ForInstructionContext forInstruction() throws RecognitionException {
		ForInstructionContext _localctx = new ForInstructionContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_forInstruction);
		int _la;
		try {
			_localctx = new ForStmtContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(214);
			match(T__16);
			setState(219);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,28,_ctx) ) {
			case 1:
				{
				setState(215);
				match(T__4);
				setState(216);
				forDeclare();
				setState(217);
				match(T__0);
				}
				break;
			}
			setState(221);
			expr(0);
			setState(226);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__0) {
				{
				setState(222);
				match(T__0);
				setState(223);
				forDeclare();
				setState(224);
				match(T__5);
				}
			}

			setState(228);
			match(T__1);
			setState(229);
			((ForStmtContext)_localctx).lsfor = instruction();
			setState(230);
			match(T__2);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ForDeclareContext extends ParserRuleContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ForDeclareContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_forDeclare; }
	}

	public final ForDeclareContext forDeclare() throws RecognitionException {
		ForDeclareContext _localctx = new ForDeclareContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_forDeclare);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(232);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class SwitchInstructionContext extends ParserRuleContext {
		public SwitchInstructionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_switchInstruction; }
	 
		public SwitchInstructionContext() { }
		public void copyFrom(SwitchInstructionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class SwitchStmtContext extends SwitchInstructionContext {
		public InstructionContext lsDefautl;
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public CasesContext cases() {
			return getRuleContext(CasesContext.class,0);
		}
		public InstructionContext instruction() {
			return getRuleContext(InstructionContext.class,0);
		}
		public SwitchStmtContext(SwitchInstructionContext ctx) { copyFrom(ctx); }
	}

	public final SwitchInstructionContext switchInstruction() throws RecognitionException {
		SwitchInstructionContext _localctx = new SwitchInstructionContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_switchInstruction);
		int _la;
		try {
			_localctx = new SwitchStmtContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(234);
			match(T__17);
			setState(235);
			expr(0);
			setState(236);
			match(T__1);
			setState(237);
			cases();
			setState(241);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__18) {
				{
				setState(238);
				match(T__18);
				setState(239);
				match(T__6);
				setState(240);
				((SwitchStmtContext)_localctx).lsDefautl = instruction();
				}
			}

			setState(243);
			match(T__2);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class CasesContext extends ParserRuleContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public List<InstructionContext> instruction() {
			return getRuleContexts(InstructionContext.class);
		}
		public InstructionContext instruction(int i) {
			return getRuleContext(InstructionContext.class,i);
		}
		public CasesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_cases; }
	}

	public final CasesContext cases() throws RecognitionException {
		CasesContext _localctx = new CasesContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_cases);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(250); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(245);
				match(T__19);
				setState(246);
				expr(0);
				setState(247);
				match(T__6);
				setState(248);
				instruction();
				}
				}
				setState(252); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==T__19 );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class BreakInstructionContext extends ParserRuleContext {
		public BreakInstructionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_breakInstruction; }
	 
		public BreakInstructionContext() { }
		public void copyFrom(BreakInstructionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class BreakTransferContext extends BreakInstructionContext {
		public BreakTransferContext(BreakInstructionContext ctx) { copyFrom(ctx); }
	}

	public final BreakInstructionContext breakInstruction() throws RecognitionException {
		BreakInstructionContext _localctx = new BreakInstructionContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_breakInstruction);
		try {
			_localctx = new BreakTransferContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(254);
			match(T__20);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ContinueInstructionContext extends ParserRuleContext {
		public ContinueInstructionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_continueInstruction; }
	 
		public ContinueInstructionContext() { }
		public void copyFrom(ContinueInstructionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ContinueTransferContext extends ContinueInstructionContext {
		public ContinueTransferContext(ContinueInstructionContext ctx) { copyFrom(ctx); }
	}

	public final ContinueInstructionContext continueInstruction() throws RecognitionException {
		ContinueInstructionContext _localctx = new ContinueInstructionContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_continueInstruction);
		try {
			_localctx = new ContinueTransferContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(256);
			match(T__21);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ReturnFuncContext extends ParserRuleContext {
		public ReturnFuncContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_returnFunc; }
	 
		public ReturnFuncContext() { }
		public void copyFrom(ReturnFuncContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ReturnStmtContext extends ReturnFuncContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ReturnStmtContext(ReturnFuncContext ctx) { copyFrom(ctx); }
	}

	public final ReturnFuncContext returnFunc() throws RecognitionException {
		ReturnFuncContext _localctx = new ReturnFuncContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_returnFunc);
		try {
			_localctx = new ReturnStmtContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(258);
			match(T__22);
			setState(260);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,32,_ctx) ) {
			case 1:
				{
				setState(259);
				expr(0);
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ExpressionContext extends ParserRuleContext {
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	 
		public ExpressionContext() { }
		public void copyFrom(ExpressionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class StmtExprContext extends ExpressionContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public StmtExprContext(ExpressionContext ctx) { copyFrom(ctx); }
	}

	public final ExpressionContext expression() throws RecognitionException {
		ExpressionContext _localctx = new ExpressionContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_expression);
		try {
			_localctx = new StmtExprContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(262);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class TypeContext extends ParserRuleContext {
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
	}

	public final TypeContext type() throws RecognitionException {
		TypeContext _localctx = new TypeContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_type);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(264);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 520093696L) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ExprContext extends ParserRuleContext {
		public ExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expr; }
	 
		public ExprContext() { }
		public void copyFrom(ExprContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class RelationalOperatorContext extends ExprContext {
		public ExprContext left;
		public Token operador;
		public ExprContext right;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public RelationalOperatorContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class NullContext extends ExprContext {
		public TerminalNode NULL() { return getToken(lexicalAnalyzerParser.NULL, 0); }
		public NullContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class CharacterContext extends ExprContext {
		public TerminalNode CHARACTER() { return getToken(lexicalAnalyzerParser.CHARACTER, 0); }
		public CharacterContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class TypeOfContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public TypeOfContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class MulDivContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public MulDivContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class AddSubContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public AddSubContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ParensContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ParensContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ConvertIntContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ConvertIntContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class LogicOperatorContext extends ExprContext {
		public ExprContext left;
		public Token operador;
		public ExprContext right;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public LogicOperatorContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class StringContext extends ExprContext {
		public TerminalNode STRING() { return getToken(lexicalAnalyzerParser.STRING, 0); }
		public StringContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class IdentifierContext extends ExprContext {
		public TerminalNode ID() { return getToken(lexicalAnalyzerParser.ID, 0); }
		public IdentifierContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class NumberContext extends ExprContext {
		public TerminalNode INT() { return getToken(lexicalAnalyzerParser.INT, 0); }
		public NumberContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class DecimalContext extends ExprContext {
		public TerminalNode DECIMAL() { return getToken(lexicalAnalyzerParser.DECIMAL, 0); }
		public DecimalContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class NegateOperatorContext extends ExprContext {
		public ExprContext right;
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public NegateOperatorContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ConvertFloatContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ConvertFloatContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class AssingVarContext extends ExprContext {
		public TerminalNode ID() { return getToken(lexicalAnalyzerParser.ID, 0); }
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public AssingVarContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class NegateContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public NegateContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class FunctionCallContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public List<FuncContext> func() {
			return getRuleContexts(FuncContext.class);
		}
		public FuncContext func(int i) {
			return getRuleContext(FuncContext.class,i);
		}
		public FunctionCallContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class BooleanContext extends ExprContext {
		public TerminalNode BOOL() { return getToken(lexicalAnalyzerParser.BOOL, 0); }
		public BooleanContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ModuleContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public ModuleContext(ExprContext ctx) { copyFrom(ctx); }
	}

	public final ExprContext expr() throws RecognitionException {
		return expr(0);
	}

	private ExprContext expr(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExprContext _localctx = new ExprContext(_ctx, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 42;
		enterRecursionRule(_localctx, 42, RULE_expr, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(302);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,34,_ctx) ) {
			case 1:
				{
				_localctx = new NegateContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(267);
				match(T__28);
				setState(268);
				expr(26);
				}
				break;
			case 2:
				{
				_localctx = new NegateOperatorContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(269);
				match(T__33);
				setState(270);
				((NegateOperatorContext)_localctx).right = expr(21);
				}
				break;
			case 3:
				{
				_localctx = new ConvertIntContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(271);
				match(T__34);
				setState(272);
				match(T__4);
				setState(273);
				expr(0);
				setState(274);
				match(T__5);
				}
				break;
			case 4:
				{
				_localctx = new ConvertFloatContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(276);
				match(T__35);
				setState(277);
				match(T__4);
				setState(278);
				expr(0);
				setState(279);
				match(T__5);
				}
				break;
			case 5:
				{
				_localctx = new TypeOfContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(281);
				match(T__36);
				setState(282);
				match(T__4);
				setState(283);
				expr(0);
				setState(284);
				match(T__5);
				}
				break;
			case 6:
				{
				_localctx = new AssingVarContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(286);
				match(ID);
				setState(287);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 1055531162667008L) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(289);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,33,_ctx) ) {
				case 1:
					{
					setState(288);
					expr(0);
					}
					break;
				}
				}
				break;
			case 7:
				{
				_localctx = new BooleanContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(291);
				match(BOOL);
				}
				break;
			case 8:
				{
				_localctx = new NullContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(292);
				match(NULL);
				}
				break;
			case 9:
				{
				_localctx = new IdentifierContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(293);
				match(ID);
				}
				break;
			case 10:
				{
				_localctx = new NumberContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(294);
				match(INT);
				}
				break;
			case 11:
				{
				_localctx = new DecimalContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(295);
				match(DECIMAL);
				}
				break;
			case 12:
				{
				_localctx = new CharacterContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(296);
				match(CHARACTER);
				}
				break;
			case 13:
				{
				_localctx = new StringContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(297);
				match(STRING);
				}
				break;
			case 14:
				{
				_localctx = new ParensContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(298);
				match(T__4);
				setState(299);
				expr(0);
				setState(300);
				match(T__5);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(345);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,37,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(343);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,36,_ctx) ) {
					case 1:
						{
						_localctx = new MulDivContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(304);
						if (!(precpred(_ctx, 24))) throw new FailedPredicateException(this, "precpred(_ctx, 24)");
						setState(305);
						_la = _input.LA(1);
						if ( !(_la==T__29 || _la==T__30) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(306);
						expr(25);
						}
						break;
					case 2:
						{
						_localctx = new AddSubContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(307);
						if (!(precpred(_ctx, 23))) throw new FailedPredicateException(this, "precpred(_ctx, 23)");
						setState(308);
						_la = _input.LA(1);
						if ( !(_la==T__28 || _la==T__31) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(309);
						expr(24);
						}
						break;
					case 3:
						{
						_localctx = new ModuleContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(310);
						if (!(precpred(_ctx, 22))) throw new FailedPredicateException(this, "precpred(_ctx, 22)");
						setState(311);
						match(T__32);
						setState(312);
						expr(23);
						}
						break;
					case 4:
						{
						_localctx = new RelationalOperatorContext(new ExprContext(_parentctx, _parentState));
						((RelationalOperatorContext)_localctx).left = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(313);
						if (!(precpred(_ctx, 17))) throw new FailedPredicateException(this, "precpred(_ctx, 17)");
						setState(314);
						((RelationalOperatorContext)_localctx).operador = match(T__37);
						setState(315);
						((RelationalOperatorContext)_localctx).right = expr(18);
						}
						break;
					case 5:
						{
						_localctx = new RelationalOperatorContext(new ExprContext(_parentctx, _parentState));
						((RelationalOperatorContext)_localctx).left = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(316);
						if (!(precpred(_ctx, 16))) throw new FailedPredicateException(this, "precpred(_ctx, 16)");
						setState(317);
						((RelationalOperatorContext)_localctx).operador = match(T__38);
						setState(318);
						((RelationalOperatorContext)_localctx).right = expr(17);
						}
						break;
					case 6:
						{
						_localctx = new RelationalOperatorContext(new ExprContext(_parentctx, _parentState));
						((RelationalOperatorContext)_localctx).left = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(319);
						if (!(precpred(_ctx, 15))) throw new FailedPredicateException(this, "precpred(_ctx, 15)");
						setState(320);
						((RelationalOperatorContext)_localctx).operador = match(T__39);
						setState(321);
						((RelationalOperatorContext)_localctx).right = expr(16);
						}
						break;
					case 7:
						{
						_localctx = new RelationalOperatorContext(new ExprContext(_parentctx, _parentState));
						((RelationalOperatorContext)_localctx).left = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(322);
						if (!(precpred(_ctx, 14))) throw new FailedPredicateException(this, "precpred(_ctx, 14)");
						setState(323);
						((RelationalOperatorContext)_localctx).operador = match(T__40);
						setState(324);
						((RelationalOperatorContext)_localctx).right = expr(15);
						}
						break;
					case 8:
						{
						_localctx = new RelationalOperatorContext(new ExprContext(_parentctx, _parentState));
						((RelationalOperatorContext)_localctx).left = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(325);
						if (!(precpred(_ctx, 13))) throw new FailedPredicateException(this, "precpred(_ctx, 13)");
						setState(326);
						((RelationalOperatorContext)_localctx).operador = match(T__41);
						setState(327);
						((RelationalOperatorContext)_localctx).right = expr(14);
						}
						break;
					case 9:
						{
						_localctx = new RelationalOperatorContext(new ExprContext(_parentctx, _parentState));
						((RelationalOperatorContext)_localctx).left = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(328);
						if (!(precpred(_ctx, 12))) throw new FailedPredicateException(this, "precpred(_ctx, 12)");
						setState(329);
						((RelationalOperatorContext)_localctx).operador = match(T__42);
						setState(330);
						((RelationalOperatorContext)_localctx).right = expr(13);
						}
						break;
					case 10:
						{
						_localctx = new LogicOperatorContext(new ExprContext(_parentctx, _parentState));
						((LogicOperatorContext)_localctx).left = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(331);
						if (!(precpred(_ctx, 11))) throw new FailedPredicateException(this, "precpred(_ctx, 11)");
						setState(332);
						((LogicOperatorContext)_localctx).operador = match(T__43);
						setState(333);
						((LogicOperatorContext)_localctx).right = expr(12);
						}
						break;
					case 11:
						{
						_localctx = new LogicOperatorContext(new ExprContext(_parentctx, _parentState));
						((LogicOperatorContext)_localctx).left = _prevctx;
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(334);
						if (!(precpred(_ctx, 10))) throw new FailedPredicateException(this, "precpred(_ctx, 10)");
						setState(335);
						((LogicOperatorContext)_localctx).operador = match(T__44);
						setState(336);
						((LogicOperatorContext)_localctx).right = expr(11);
						}
						break;
					case 12:
						{
						_localctx = new FunctionCallContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(337);
						if (!(precpred(_ctx, 25))) throw new FailedPredicateException(this, "precpred(_ctx, 25)");
						setState(339); 
						_errHandler.sync(this);
						_alt = 1;
						do {
							switch (_alt) {
							case 1:
								{
								{
								setState(338);
								func();
								}
								}
								break;
							default:
								throw new NoViableAltException(this);
							}
							setState(341); 
							_errHandler.sync(this);
							_alt = getInterpreter().adaptivePredict(_input,35,_ctx);
						} while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER );
						}
						break;
					}
					} 
				}
				setState(347);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,37,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 21:
			return expr_sempred((ExprContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 24);
		case 1:
			return precpred(_ctx, 23);
		case 2:
			return precpred(_ctx, 22);
		case 3:
			return precpred(_ctx, 17);
		case 4:
			return precpred(_ctx, 16);
		case 5:
			return precpred(_ctx, 15);
		case 6:
			return precpred(_ctx, 14);
		case 7:
			return precpred(_ctx, 13);
		case 8:
			return precpred(_ctx, 12);
		case 9:
			return precpred(_ctx, 11);
		case 10:
			return precpred(_ctx, 10);
		case 11:
			return precpred(_ctx, 25);
		}
		return true;
	}

	public static final String _serializedATN =
		"\u0004\u0001;\u015d\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004\u0002"+
		"\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007\u0007\u0007\u0002"+
		"\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b\u0007\u000b\u0002"+
		"\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002\u000f\u0007\u000f"+
		"\u0002\u0010\u0007\u0010\u0002\u0011\u0007\u0011\u0002\u0012\u0007\u0012"+
		"\u0002\u0013\u0007\u0013\u0002\u0014\u0007\u0014\u0002\u0015\u0007\u0015"+
		"\u0001\u0000\u0005\u0000.\b\u0000\n\u0000\f\u00001\t\u0000\u0001\u0001"+
		"\u0001\u0001\u0003\u00015\b\u0001\u0001\u0001\u0001\u0001\u0003\u0001"+
		"9\b\u0001\u0001\u0001\u0001\u0001\u0003\u0001=\b\u0001\u0001\u0001\u0001"+
		"\u0001\u0003\u0001A\b\u0001\u0001\u0001\u0001\u0001\u0003\u0001E\b\u0001"+
		"\u0001\u0001\u0001\u0001\u0003\u0001I\b\u0001\u0001\u0001\u0001\u0001"+
		"\u0003\u0001M\b\u0001\u0001\u0001\u0001\u0001\u0003\u0001Q\b\u0001\u0001"+
		"\u0001\u0001\u0001\u0003\u0001U\b\u0001\u0001\u0001\u0001\u0001\u0003"+
		"\u0001Y\b\u0001\u0001\u0001\u0001\u0001\u0003\u0001]\b\u0001\u0001\u0001"+
		"\u0001\u0001\u0003\u0001a\b\u0001\u0001\u0001\u0001\u0001\u0003\u0001"+
		"e\b\u0001\u0003\u0001g\b\u0001\u0001\u0002\u0001\u0002\u0005\u0002k\b"+
		"\u0002\n\u0002\f\u0002n\t\u0002\u0001\u0002\u0001\u0002\u0001\u0003\u0001"+
		"\u0003\u0001\u0003\u0001\u0003\u0003\u0003v\b\u0003\u0001\u0003\u0001"+
		"\u0003\u0001\u0003\u0003\u0003{\b\u0003\u0001\u0003\u0001\u0003\u0001"+
		"\u0003\u0001\u0003\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0001"+
		"\u0004\u0005\u0004\u0086\b\u0004\n\u0004\f\u0004\u0089\t\u0004\u0001\u0005"+
		"\u0001\u0005\u0003\u0005\u008d\b\u0005\u0001\u0005\u0001\u0005\u0001\u0006"+
		"\u0001\u0006\u0001\u0006\u0005\u0006\u0094\b\u0006\n\u0006\f\u0006\u0097"+
		"\t\u0006\u0001\u0007\u0001\u0007\u0001\u0007\u0001\u0007\u0001\u0007\u0001"+
		"\u0007\u0001\u0007\u0001\u0007\u0005\u0007\u00a1\b\u0007\n\u0007\f\u0007"+
		"\u00a4\t\u0007\u0001\u0007\u0003\u0007\u00a7\b\u0007\u0001\b\u0001\b\u0001"+
		"\b\u0001\b\u0001\b\u0003\b\u00ae\b\b\u0001\t\u0001\t\u0001\t\u0001\t\u0001"+
		"\n\u0001\n\u0001\n\u0001\n\u0001\n\u0005\n\u00b9\b\n\n\n\f\n\u00bc\t\n"+
		"\u0001\n\u0001\n\u0001\u000b\u0001\u000b\u0001\u000b\u0001\u000b\u0001"+
		"\u000b\u0001\u000b\u0001\u000b\u0001\u000b\u0001\u000b\u0001\u000b\u0001"+
		"\u000b\u0005\u000b\u00cb\b\u000b\n\u000b\f\u000b\u00ce\t\u000b\u0001\u000b"+
		"\u0001\u000b\u0001\u000b\u0001\u000b\u0001\u000b\u0003\u000b\u00d5\b\u000b"+
		"\u0001\f\u0001\f\u0001\f\u0001\f\u0001\f\u0003\f\u00dc\b\f\u0001\f\u0001"+
		"\f\u0001\f\u0001\f\u0001\f\u0003\f\u00e3\b\f\u0001\f\u0001\f\u0001\f\u0001"+
		"\f\u0001\r\u0001\r\u0001\u000e\u0001\u000e\u0001\u000e\u0001\u000e\u0001"+
		"\u000e\u0001\u000e\u0001\u000e\u0003\u000e\u00f2\b\u000e\u0001\u000e\u0001"+
		"\u000e\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u000f\u0004"+
		"\u000f\u00fb\b\u000f\u000b\u000f\f\u000f\u00fc\u0001\u0010\u0001\u0010"+
		"\u0001\u0011\u0001\u0011\u0001\u0012\u0001\u0012\u0003\u0012\u0105\b\u0012"+
		"\u0001\u0013\u0001\u0013\u0001\u0014\u0001\u0014\u0001\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0003\u0015\u0122\b\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0003\u0015\u012f\b\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0004\u0015"+
		"\u0154\b\u0015\u000b\u0015\f\u0015\u0155\u0005\u0015\u0158\b\u0015\n\u0015"+
		"\f\u0015\u015b\t\u0015\u0001\u0015\u0000\u0001*\u0016\u0000\u0002\u0004"+
		"\u0006\b\n\f\u000e\u0010\u0012\u0014\u0016\u0018\u001a\u001c\u001e \""+
		"$&(*\u0000\u0004\u0001\u0000\u0018\u001c\u0002\u0000\u000b\u000b.1\u0001"+
		"\u0000\u001e\u001f\u0002\u0000\u001d\u001d  \u018d\u0000/\u0001\u0000"+
		"\u0000\u0000\u0002f\u0001\u0000\u0000\u0000\u0004h\u0001\u0000\u0000\u0000"+
		"\u0006q\u0001\u0000\u0000\u0000\b\u0080\u0001\u0000\u0000\u0000\n\u008a"+
		"\u0001\u0000\u0000\u0000\f\u0090\u0001\u0000\u0000\u0000\u000e\u0098\u0001"+
		"\u0000\u0000\u0000\u0010\u00a8\u0001\u0000\u0000\u0000\u0012\u00af\u0001"+
		"\u0000\u0000\u0000\u0014\u00b3\u0001\u0000\u0000\u0000\u0016\u00bf\u0001"+
		"\u0000\u0000\u0000\u0018\u00d6\u0001\u0000\u0000\u0000\u001a\u00e8\u0001"+
		"\u0000\u0000\u0000\u001c\u00ea\u0001\u0000\u0000\u0000\u001e\u00fa\u0001"+
		"\u0000\u0000\u0000 \u00fe\u0001\u0000\u0000\u0000\"\u0100\u0001\u0000"+
		"\u0000\u0000$\u0102\u0001\u0000\u0000\u0000&\u0106\u0001\u0000\u0000\u0000"+
		"(\u0108\u0001\u0000\u0000\u0000*\u012e\u0001\u0000\u0000\u0000,.\u0003"+
		"\u0002\u0001\u0000-,\u0001\u0000\u0000\u0000.1\u0001\u0000\u0000\u0000"+
		"/-\u0001\u0000\u0000\u0000/0\u0001\u0000\u0000\u00000\u0001\u0001\u0000"+
		"\u0000\u00001/\u0001\u0000\u0000\u000024\u0003&\u0013\u000035\u0005\u0001"+
		"\u0000\u000043\u0001\u0000\u0000\u000045\u0001\u0000\u0000\u00005g\u0001"+
		"\u0000\u0000\u000068\u0003\u0010\b\u000079\u0005\u0001\u0000\u000087\u0001"+
		"\u0000\u0000\u000089\u0001\u0000\u0000\u00009g\u0001\u0000\u0000\u0000"+
		":<\u0003\u0012\t\u0000;=\u0005\u0001\u0000\u0000<;\u0001\u0000\u0000\u0000"+
		"<=\u0001\u0000\u0000\u0000=g\u0001\u0000\u0000\u0000>@\u0003\u0006\u0003"+
		"\u0000?A\u0005\u0001\u0000\u0000@?\u0001\u0000\u0000\u0000@A\u0001\u0000"+
		"\u0000\u0000Ag\u0001\u0000\u0000\u0000BD\u0003\u0014\n\u0000CE\u0005\u0001"+
		"\u0000\u0000DC\u0001\u0000\u0000\u0000DE\u0001\u0000\u0000\u0000Eg\u0001"+
		"\u0000\u0000\u0000FH\u0003\u0016\u000b\u0000GI\u0005\u0001\u0000\u0000"+
		"HG\u0001\u0000\u0000\u0000HI\u0001\u0000\u0000\u0000Ig\u0001\u0000\u0000"+
		"\u0000JL\u0003\u000e\u0007\u0000KM\u0005\u0001\u0000\u0000LK\u0001\u0000"+
		"\u0000\u0000LM\u0001\u0000\u0000\u0000Mg\u0001\u0000\u0000\u0000NP\u0003"+
		"\u0018\f\u0000OQ\u0005\u0001\u0000\u0000PO\u0001\u0000\u0000\u0000PQ\u0001"+
		"\u0000\u0000\u0000Qg\u0001\u0000\u0000\u0000RT\u0003\u001c\u000e\u0000"+
		"SU\u0005\u0001\u0000\u0000TS\u0001\u0000\u0000\u0000TU\u0001\u0000\u0000"+
		"\u0000Ug\u0001\u0000\u0000\u0000VX\u0003 \u0010\u0000WY\u0005\u0001\u0000"+
		"\u0000XW\u0001\u0000\u0000\u0000XY\u0001\u0000\u0000\u0000Yg\u0001\u0000"+
		"\u0000\u0000Z\\\u0003\"\u0011\u0000[]\u0005\u0001\u0000\u0000\\[\u0001"+
		"\u0000\u0000\u0000\\]\u0001\u0000\u0000\u0000]g\u0001\u0000\u0000\u0000"+
		"^`\u0003$\u0012\u0000_a\u0005\u0001\u0000\u0000`_\u0001\u0000\u0000\u0000"+
		"`a\u0001\u0000\u0000\u0000ag\u0001\u0000\u0000\u0000bd\u0003\u0004\u0002"+
		"\u0000ce\u0005\u0001\u0000\u0000dc\u0001\u0000\u0000\u0000de\u0001\u0000"+
		"\u0000\u0000eg\u0001\u0000\u0000\u0000f2\u0001\u0000\u0000\u0000f6\u0001"+
		"\u0000\u0000\u0000f:\u0001\u0000\u0000\u0000f>\u0001\u0000\u0000\u0000"+
		"fB\u0001\u0000\u0000\u0000fF\u0001\u0000\u0000\u0000fJ\u0001\u0000\u0000"+
		"\u0000fN\u0001\u0000\u0000\u0000fR\u0001\u0000\u0000\u0000fV\u0001\u0000"+
		"\u0000\u0000fZ\u0001\u0000\u0000\u0000f^\u0001\u0000\u0000\u0000fb\u0001"+
		"\u0000\u0000\u0000g\u0003\u0001\u0000\u0000\u0000hl\u0005\u0002\u0000"+
		"\u0000ik\u0003\u0002\u0001\u0000ji\u0001\u0000\u0000\u0000kn\u0001\u0000"+
		"\u0000\u0000lj\u0001\u0000\u0000\u0000lm\u0001\u0000\u0000\u0000mo\u0001"+
		"\u0000\u0000\u0000nl\u0001\u0000\u0000\u0000op\u0005\u0003\u0000\u0000"+
		"p\u0005\u0001\u0000\u0000\u0000qr\u0005\u0004\u0000\u0000rs\u00057\u0000"+
		"\u0000su\u0005\u0005\u0000\u0000tv\u0003\b\u0004\u0000ut\u0001\u0000\u0000"+
		"\u0000uv\u0001\u0000\u0000\u0000vw\u0001\u0000\u0000\u0000wx\u0005\u0006"+
		"\u0000\u0000xz\u0005\u0007\u0000\u0000y{\u0003(\u0014\u0000zy\u0001\u0000"+
		"\u0000\u0000z{\u0001\u0000\u0000\u0000{|\u0001\u0000\u0000\u0000|}\u0005"+
		"\u0002\u0000\u0000}~\u0003\u0002\u0001\u0000~\u007f\u0005\u0003\u0000"+
		"\u0000\u007f\u0007\u0001\u0000\u0000\u0000\u0080\u0081\u00057\u0000\u0000"+
		"\u0081\u0087\u0003(\u0014\u0000\u0082\u0083\u0005\b\u0000\u0000\u0083"+
		"\u0084\u00057\u0000\u0000\u0084\u0086\u0003(\u0014\u0000\u0085\u0082\u0001"+
		"\u0000\u0000\u0000\u0086\u0089\u0001\u0000\u0000\u0000\u0087\u0085\u0001"+
		"\u0000\u0000\u0000\u0087\u0088\u0001\u0000\u0000\u0000\u0088\t\u0001\u0000"+
		"\u0000\u0000\u0089\u0087\u0001\u0000\u0000\u0000\u008a\u008c\u0005\u0005"+
		"\u0000\u0000\u008b\u008d\u0003\f\u0006\u0000\u008c\u008b\u0001\u0000\u0000"+
		"\u0000\u008c\u008d\u0001\u0000\u0000\u0000\u008d\u008e\u0001\u0000\u0000"+
		"\u0000\u008e\u008f\u0005\u0006\u0000\u0000\u008f\u000b\u0001\u0000\u0000"+
		"\u0000\u0090\u0095\u0003*\u0015\u0000\u0091\u0092\u0005\b\u0000\u0000"+
		"\u0092\u0094\u0003*\u0015\u0000\u0093\u0091\u0001\u0000\u0000\u0000\u0094"+
		"\u0097\u0001\u0000\u0000\u0000\u0095\u0093\u0001\u0000\u0000\u0000\u0095"+
		"\u0096\u0001\u0000\u0000\u0000\u0096\r\u0001\u0000\u0000\u0000\u0097\u0095"+
		"\u0001\u0000\u0000\u0000\u0098\u0099\u0005\t\u0000\u0000\u0099\u009a\u0005"+
		"7\u0000\u0000\u009a\u009b\u0005\n\u0000\u0000\u009b\u00a6\u0003(\u0014"+
		"\u0000\u009c\u009d\u0005\u000b\u0000\u0000\u009d\u00a2\u0005\u0002\u0000"+
		"\u0000\u009e\u00a1\u0003*\u0015\u0000\u009f\u00a1\u0005\b\u0000\u0000"+
		"\u00a0\u009e\u0001\u0000\u0000\u0000\u00a0\u009f\u0001\u0000\u0000\u0000"+
		"\u00a1\u00a4\u0001\u0000\u0000\u0000\u00a2\u00a0\u0001\u0000\u0000\u0000"+
		"\u00a2\u00a3\u0001\u0000\u0000\u0000\u00a3\u00a5\u0001\u0000\u0000\u0000"+
		"\u00a4\u00a2\u0001\u0000\u0000\u0000\u00a5\u00a7\u0005\u0003\u0000\u0000"+
		"\u00a6\u009c\u0001\u0000\u0000\u0000\u00a6\u00a7\u0001\u0000\u0000\u0000"+
		"\u00a7\u000f\u0001\u0000\u0000\u0000\u00a8\u00a9\u0005\t\u0000\u0000\u00a9"+
		"\u00aa\u00057\u0000\u0000\u00aa\u00ad\u0003(\u0014\u0000\u00ab\u00ac\u0005"+
		"\u000b\u0000\u0000\u00ac\u00ae\u0003*\u0015\u0000\u00ad\u00ab\u0001\u0000"+
		"\u0000\u0000\u00ad\u00ae\u0001\u0000\u0000\u0000\u00ae\u0011\u0001\u0000"+
		"\u0000\u0000\u00af\u00b0\u00057\u0000\u0000\u00b0\u00b1\u0005\f\u0000"+
		"\u0000\u00b1\u00b2\u0003*\u0015\u0000\u00b2\u0013\u0001\u0000\u0000\u0000"+
		"\u00b3\u00b4\u0005\r\u0000\u0000\u00b4\u00b5\u0005\u0005\u0000\u0000\u00b5"+
		"\u00ba\u0003*\u0015\u0000\u00b6\u00b7\u0005\b\u0000\u0000\u00b7\u00b9"+
		"\u0003*\u0015\u0000\u00b8\u00b6\u0001\u0000\u0000\u0000\u00b9\u00bc\u0001"+
		"\u0000\u0000\u0000\u00ba\u00b8\u0001\u0000\u0000\u0000\u00ba\u00bb\u0001"+
		"\u0000\u0000\u0000\u00bb\u00bd\u0001\u0000\u0000\u0000\u00bc\u00ba\u0001"+
		"\u0000\u0000\u0000\u00bd\u00be\u0005\u0006\u0000\u0000\u00be\u0015\u0001"+
		"\u0000\u0000\u0000\u00bf\u00c0\u0005\u000e\u0000\u0000\u00c0\u00c1\u0003"+
		"*\u0015\u0000\u00c1\u00c2\u0005\u0002\u0000\u0000\u00c2\u00c3\u0003\u0002"+
		"\u0001\u0000\u00c3\u00cc\u0005\u0003\u0000\u0000\u00c4\u00c5\u0005\u000f"+
		"\u0000\u0000\u00c5\u00c6\u0003*\u0015\u0000\u00c6\u00c7\u0005\u0002\u0000"+
		"\u0000\u00c7\u00c8\u0003\u0002\u0001\u0000\u00c8\u00c9\u0005\u0003\u0000"+
		"\u0000\u00c9\u00cb\u0001\u0000\u0000\u0000\u00ca\u00c4\u0001\u0000\u0000"+
		"\u0000\u00cb\u00ce\u0001\u0000\u0000\u0000\u00cc\u00ca\u0001\u0000\u0000"+
		"\u0000\u00cc\u00cd\u0001\u0000\u0000\u0000\u00cd\u00d4\u0001\u0000\u0000"+
		"\u0000\u00ce\u00cc\u0001\u0000\u0000\u0000\u00cf\u00d0\u0005\u0010\u0000"+
		"\u0000\u00d0\u00d1\u0005\u0002\u0000\u0000\u00d1\u00d2\u0003\u0002\u0001"+
		"\u0000\u00d2\u00d3\u0005\u0003\u0000\u0000\u00d3\u00d5\u0001\u0000\u0000"+
		"\u0000\u00d4\u00cf\u0001\u0000\u0000\u0000\u00d4\u00d5\u0001\u0000\u0000"+
		"\u0000\u00d5\u0017\u0001\u0000\u0000\u0000\u00d6\u00db\u0005\u0011\u0000"+
		"\u0000\u00d7\u00d8\u0005\u0005\u0000\u0000\u00d8\u00d9\u0003\u001a\r\u0000"+
		"\u00d9\u00da\u0005\u0001\u0000\u0000\u00da\u00dc\u0001\u0000\u0000\u0000"+
		"\u00db\u00d7\u0001\u0000\u0000\u0000\u00db\u00dc\u0001\u0000\u0000\u0000"+
		"\u00dc\u00dd\u0001\u0000\u0000\u0000\u00dd\u00e2\u0003*\u0015\u0000\u00de"+
		"\u00df\u0005\u0001\u0000\u0000\u00df\u00e0\u0003\u001a\r\u0000\u00e0\u00e1"+
		"\u0005\u0006\u0000\u0000\u00e1\u00e3\u0001\u0000\u0000\u0000\u00e2\u00de"+
		"\u0001\u0000\u0000\u0000\u00e2\u00e3\u0001\u0000\u0000\u0000\u00e3\u00e4"+
		"\u0001\u0000\u0000\u0000\u00e4\u00e5\u0005\u0002\u0000\u0000\u00e5\u00e6"+
		"\u0003\u0002\u0001\u0000\u00e6\u00e7\u0005\u0003\u0000\u0000\u00e7\u0019"+
		"\u0001\u0000\u0000\u0000\u00e8\u00e9\u0003*\u0015\u0000\u00e9\u001b\u0001"+
		"\u0000\u0000\u0000\u00ea\u00eb\u0005\u0012\u0000\u0000\u00eb\u00ec\u0003"+
		"*\u0015\u0000\u00ec\u00ed\u0005\u0002\u0000\u0000\u00ed\u00f1\u0003\u001e"+
		"\u000f\u0000\u00ee\u00ef\u0005\u0013\u0000\u0000\u00ef\u00f0\u0005\u0007"+
		"\u0000\u0000\u00f0\u00f2\u0003\u0002\u0001\u0000\u00f1\u00ee\u0001\u0000"+
		"\u0000\u0000\u00f1\u00f2\u0001\u0000\u0000\u0000\u00f2\u00f3\u0001\u0000"+
		"\u0000\u0000\u00f3\u00f4\u0005\u0003\u0000\u0000\u00f4\u001d\u0001\u0000"+
		"\u0000\u0000\u00f5\u00f6\u0005\u0014\u0000\u0000\u00f6\u00f7\u0003*\u0015"+
		"\u0000\u00f7\u00f8\u0005\u0007\u0000\u0000\u00f8\u00f9\u0003\u0002\u0001"+
		"\u0000\u00f9\u00fb\u0001\u0000\u0000\u0000\u00fa\u00f5\u0001\u0000\u0000"+
		"\u0000\u00fb\u00fc\u0001\u0000\u0000\u0000\u00fc\u00fa\u0001\u0000\u0000"+
		"\u0000\u00fc\u00fd\u0001\u0000\u0000\u0000\u00fd\u001f\u0001\u0000\u0000"+
		"\u0000\u00fe\u00ff\u0005\u0015\u0000\u0000\u00ff!\u0001\u0000\u0000\u0000"+
		"\u0100\u0101\u0005\u0016\u0000\u0000\u0101#\u0001\u0000\u0000\u0000\u0102"+
		"\u0104\u0005\u0017\u0000\u0000\u0103\u0105\u0003*\u0015\u0000\u0104\u0103"+
		"\u0001\u0000\u0000\u0000\u0104\u0105\u0001\u0000\u0000\u0000\u0105%\u0001"+
		"\u0000\u0000\u0000\u0106\u0107\u0003*\u0015\u0000\u0107\'\u0001\u0000"+
		"\u0000\u0000\u0108\u0109\u0007\u0000\u0000\u0000\u0109)\u0001\u0000\u0000"+
		"\u0000\u010a\u010b\u0006\u0015\uffff\uffff\u0000\u010b\u010c\u0005\u001d"+
		"\u0000\u0000\u010c\u012f\u0003*\u0015\u001a\u010d\u010e\u0005\"\u0000"+
		"\u0000\u010e\u012f\u0003*\u0015\u0015\u010f\u0110\u0005#\u0000\u0000\u0110"+
		"\u0111\u0005\u0005\u0000\u0000\u0111\u0112\u0003*\u0015\u0000\u0112\u0113"+
		"\u0005\u0006\u0000\u0000\u0113\u012f\u0001\u0000\u0000\u0000\u0114\u0115"+
		"\u0005$\u0000\u0000\u0115\u0116\u0005\u0005\u0000\u0000\u0116\u0117\u0003"+
		"*\u0015\u0000\u0117\u0118\u0005\u0006\u0000\u0000\u0118\u012f\u0001\u0000"+
		"\u0000\u0000\u0119\u011a\u0005%\u0000\u0000\u011a\u011b\u0005\u0005\u0000"+
		"\u0000\u011b\u011c\u0003*\u0015\u0000\u011c\u011d\u0005\u0006\u0000\u0000"+
		"\u011d\u012f\u0001\u0000\u0000\u0000\u011e\u011f\u00057\u0000\u0000\u011f"+
		"\u0121\u0007\u0001\u0000\u0000\u0120\u0122\u0003*\u0015\u0000\u0121\u0120"+
		"\u0001\u0000\u0000\u0000\u0121\u0122\u0001\u0000\u0000\u0000\u0122\u012f"+
		"\u0001\u0000\u0000\u0000\u0123\u012f\u00056\u0000\u0000\u0124\u012f\u0005"+
		"5\u0000\u0000\u0125\u012f\u00057\u0000\u0000\u0126\u012f\u00058\u0000"+
		"\u0000\u0127\u012f\u00059\u0000\u0000\u0128\u012f\u0005:\u0000\u0000\u0129"+
		"\u012f\u0005;\u0000\u0000\u012a\u012b\u0005\u0005\u0000\u0000\u012b\u012c"+
		"\u0003*\u0015\u0000\u012c\u012d\u0005\u0006\u0000\u0000\u012d\u012f\u0001"+
		"\u0000\u0000\u0000\u012e\u010a\u0001\u0000\u0000\u0000\u012e\u010d\u0001"+
		"\u0000\u0000\u0000\u012e\u010f\u0001\u0000\u0000\u0000\u012e\u0114\u0001"+
		"\u0000\u0000\u0000\u012e\u0119\u0001\u0000\u0000\u0000\u012e\u011e\u0001"+
		"\u0000\u0000\u0000\u012e\u0123\u0001\u0000\u0000\u0000\u012e\u0124\u0001"+
		"\u0000\u0000\u0000\u012e\u0125\u0001\u0000\u0000\u0000\u012e\u0126\u0001"+
		"\u0000\u0000\u0000\u012e\u0127\u0001\u0000\u0000\u0000\u012e\u0128\u0001"+
		"\u0000\u0000\u0000\u012e\u0129\u0001\u0000\u0000\u0000\u012e\u012a\u0001"+
		"\u0000\u0000\u0000\u012f\u0159\u0001\u0000\u0000\u0000\u0130\u0131\n\u0018"+
		"\u0000\u0000\u0131\u0132\u0007\u0002\u0000\u0000\u0132\u0158\u0003*\u0015"+
		"\u0019\u0133\u0134\n\u0017\u0000\u0000\u0134\u0135\u0007\u0003\u0000\u0000"+
		"\u0135\u0158\u0003*\u0015\u0018\u0136\u0137\n\u0016\u0000\u0000\u0137"+
		"\u0138\u0005!\u0000\u0000\u0138\u0158\u0003*\u0015\u0017\u0139\u013a\n"+
		"\u0011\u0000\u0000\u013a\u013b\u0005&\u0000\u0000\u013b\u0158\u0003*\u0015"+
		"\u0012\u013c\u013d\n\u0010\u0000\u0000\u013d\u013e\u0005\'\u0000\u0000"+
		"\u013e\u0158\u0003*\u0015\u0011\u013f\u0140\n\u000f\u0000\u0000\u0140"+
		"\u0141\u0005(\u0000\u0000\u0141\u0158\u0003*\u0015\u0010\u0142\u0143\n"+
		"\u000e\u0000\u0000\u0143\u0144\u0005)\u0000\u0000\u0144\u0158\u0003*\u0015"+
		"\u000f\u0145\u0146\n\r\u0000\u0000\u0146\u0147\u0005*\u0000\u0000\u0147"+
		"\u0158\u0003*\u0015\u000e\u0148\u0149\n\f\u0000\u0000\u0149\u014a\u0005"+
		"+\u0000\u0000\u014a\u0158\u0003*\u0015\r\u014b\u014c\n\u000b\u0000\u0000"+
		"\u014c\u014d\u0005,\u0000\u0000\u014d\u0158\u0003*\u0015\f\u014e\u014f"+
		"\n\n\u0000\u0000\u014f\u0150\u0005-\u0000\u0000\u0150\u0158\u0003*\u0015"+
		"\u000b\u0151\u0153\n\u0019\u0000\u0000\u0152\u0154\u0003\n\u0005\u0000"+
		"\u0153\u0152\u0001\u0000\u0000\u0000\u0154\u0155\u0001\u0000\u0000\u0000"+
		"\u0155\u0153\u0001\u0000\u0000\u0000\u0155\u0156\u0001\u0000\u0000\u0000"+
		"\u0156\u0158\u0001\u0000\u0000\u0000\u0157\u0130\u0001\u0000\u0000\u0000"+
		"\u0157\u0133\u0001\u0000\u0000\u0000\u0157\u0136\u0001\u0000\u0000\u0000"+
		"\u0157\u0139\u0001\u0000\u0000\u0000\u0157\u013c\u0001\u0000\u0000\u0000"+
		"\u0157\u013f\u0001\u0000\u0000\u0000\u0157\u0142\u0001\u0000\u0000\u0000"+
		"\u0157\u0145\u0001\u0000\u0000\u0000\u0157\u0148\u0001\u0000\u0000\u0000"+
		"\u0157\u014b\u0001\u0000\u0000\u0000\u0157\u014e\u0001\u0000\u0000\u0000"+
		"\u0157\u0151\u0001\u0000\u0000\u0000\u0158\u015b\u0001\u0000\u0000\u0000"+
		"\u0159\u0157\u0001\u0000\u0000\u0000\u0159\u015a\u0001\u0000\u0000\u0000"+
		"\u015a+\u0001\u0000\u0000\u0000\u015b\u0159\u0001\u0000\u0000\u0000&/"+
		"48<@DHLPTX\\`dfluz\u0087\u008c\u0095\u00a0\u00a2\u00a6\u00ad\u00ba\u00cc"+
		"\u00d4\u00db\u00e2\u00f1\u00fc\u0104\u0121\u012e\u0155\u0157\u0159";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}