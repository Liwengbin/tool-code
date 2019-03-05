let jsonData = [ {
	userName : 'wenbing.li',
	userPwd : '123456',
	userAction : {
		sex : 'M',
		age : 12
	},
	userTag : [ {
		userIndex : 20,
		userText : 'Java'
	}, {
		userIndex : 23,
		userText : 'JavaScript'
	} ]
} ];

/**
 * 驼峰->_
 */
function AZtoUnderLine(data) {
	if (Array.isArray(data)) {
		return data.map(AZtoUnderLine)
	}
	if (typeof data !== 'object' || !data) {
		return data
	}
	return Object.keys(data).reduce((state, key) => {
		state[key.replace(/[A-Z]/g, _ => '_' + _.toLowerCase())] = AZtoUnderLine(data[key])
		return state
	}, {})
}

// console.log(AZtoUnderLine(jsonData));

/**
 * _->驼峰
 */
function UnderLinetoAZ(data) {
	
}
