'use strict';

/**********************************************************************************************
 *                                                                                            *
 * Перед началом работы с заданием, пожалуйста ознакомьтесь с туториалом:                     *
 * https://developer.mozilla.org/en-US/docs/Web/JavaScript/Guide/Functions                    *
 * https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Function  *
 * https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/arguments      *
 * https://developer.mozilla.org/en-US/docs/Web/JavaScript/Closures                           *
 *                                                                                            *
 **********************************************************************************************/


/**
 *  Возвращает функцию, которая является композицией двух заданных функций f (x) и g (x).
 *  Результатом должена быть функцией одного аргумента (позволяет вызывать аргумент x),
 *  который работает как применение функции f к результату применения функции g к x, т.е.
 *  getComposition (f, g) (x) = f (g (x))
 *
 *
 * @param {Function} f
 * @param {Function} g
 * @return {Function}
 *
 * @example
 *   getComposition(Math.sin, Math.asin)(x) => Math.sin(Math.acos(x))
 *
 */
function getComposition(f,g) {
    return (x) => f(g(x));
}


/**
 * Возвращает функцию возведения в степень для переданного аргумента
 *
 * @param {number} exponent
 * @return {Function}
 *
 * @example
 *   var power2 = getPowerFunction(2); // => x^2
 *   power2(2) => 4
 *   power2(4) => 16
 *
 *   var power05 = getPowerFunction(0.5); // => x^0.5
 *   power05(4) => 2
 *   power05(16) => 4
 *
 */
function getPowerFunction(exponent) {
    return (x) => Math.pow(x, exponent);
}


/**
 * Возвращает полином на основании переданных аргументов
 * Подробнее: https://en.wikipedia.org/wiki/Polynomial#Definition
 *
 * @params {integer}
 * @return {Function}
 *
 * @example
 *   getPolynom(2,3,5) => y = 2*x^2 + 3*x + 5
 *   getPolynom(1,-3)  => y = x - 3
 *   getPolynom(8)     => y = 8
 *   getPolynom()      => null
 */
function getPolynom() {
    if (arguments.length == 0) {
    	return null;
    }
    return (x) => {
    	let result = 0;
		for (let i = 0; i < arguments.length; i++) {
			result += arguments[i]*Math.pow(x, arguments.length - i - 1);
		}
		return result;
    };
}


/**
 * Заменяет переданную функцию и возвращает функцию,
 * которая в первый раз вызывает переданную функцию, а затем всегда возвращает результат кэширования.
 *
 * @params {Function} func - функция для запонимания
 * @return {Function} запомненная функция
 *
 * @example
 *   var memoizer = memoize(() => Math.random());
 *   memoizer() => любое рандомное число (при первом запуске вычисляется Math.random())
 *   memoizer() => тоже раномное число (при втором запуске возвращается закешированный результат)
 *   ...
 *   memoizer() => тоже рандомное число  (при всех последующих вызовах возвращается тоже закешированный результат)
 */
function memoize(func) {
	let someRes = func();
	return () => someRes;
}


/**
 * Возвращает функцию, которая пытаеся вызвать переданную функцию, и,
 * если она выбрасывает ошибку, повторяет вызов функции заданное количество раз.
 * @param {Function} функция
 * @param {number} количество попыток
 * @return {Function}
 *
 * @example
 * var attempt = 0, retryer = retry(() => {
 *      if (++attempt % 2) throw new Error('test');
 *      else return attempt;
 * }, 2);
 * retryer() => 2
 */
function retry(func, attempts) {
    return () => {
    	let exCaught;
    	let attempt = 0;
    	do {
    		exCaught = false;
    		try {
    			return func();
    		} catch (error) {
    			exCaught = true;
    		}
    	} while (exCaught && (++attempt <= attempts));
    };    		
}


/**
 * Возвращает логирующую обертку для указанного метода,
 * Logger должен логировать начало и конец вызова указанной функции.
 * Logger должен логировать аргументы вызываемой функции.
 * Формат вывода:
 * <function name>(<arg1>, <arg2>,...,<argN>) starts
 * <function name>(<arg1>, <arg2>,...,<argN>) ends
 * 
 * @param {Function} функция
 * @param {Function} логирующая функция - функия для вывода логов с однис строковым аргументом
 * @return {Function}
 *
 * @example
 *
 * var cosLogger = logger(Math.cos, console.log);
 * var result = cosLogger(Math.PI));     // -1
 *
 * log from console.log:
 * cos(3.141592653589793) starts
 * cos(3.141592653589793) ends
 *
 */
function logger(func, logFunc) {
	return function() {
		let argsStr = JSON.stringify(Array.from(arguments)).slice(1, -1);
		logFunc(func.name + '(' + argsStr + ') starts');
		let result = func.apply(null, arguments);
		argsStr = JSON.stringify(Array.from(arguments)).slice(1, -1);
		logFunc(func.name + '(' + argsStr + ') ends');
		return result;
	}
}


/**
 * Возвращает фуункцию с частично примененными аргументами
 * @param {Function} fn
 * @return {Function}
 *
 * @example
 *   var fn = function(x1,x2,x3,x4) { return  x1 + x2 + x3 + x4; };
 *   partialUsingArguments(fn, 'a')('b','c','d') => 'abcd'
 *   partialUsingArguments(fn, 'a','b')('c','d') => 'abcd'
 *   partialUsingArguments(fn, 'a','b','c')('d') => 'abcd'
 *   partialUsingArguments(fn, 'a','b','c','d')() => 'abcd'
 */
function partialUsingArguments(fn) {
	let argv = Array.from(arguments).slice(1);
	return function() {
		return fn.apply(this, argv.concat(Array.from(arguments)));
	}
}


/**
 * Возвращает функцию IdGenerator, которая возвращает следующее целое число при каждом вызове начиная с переданного
 *
 * @param {Number} стартовое число
 * @return {Function}
 *
 * @example
 *   var getId4 = getIdGenerator(4);
 *   var getId10 = gerIdGenerator(10);
 *   getId4() => 4
 *   getId10() => 10
 *   getId4() => 5
 *   getId4() => 6
 *   getId4() => 7
 *   getId10() => 11
 */
function getIdGeneratorFunction(startFrom) {
	return () => startFrom++;
}


module.exports = {
    getComposition: getComposition,
    getPowerFunction: getPowerFunction,
    getPolynom: getPolynom,
    memoize: memoize,
    retry: retry,
    logger: logger,
    partialUsingArguments: partialUsingArguments,
    getIdGeneratorFunction: getIdGeneratorFunction,
};
