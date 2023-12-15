module.exports = {
    meta: {
        type: 'suggestion',
        docs: {
            description: 'warn if file size is above 200 lines',
            category: 'Best Practices',
            recommended: false
        },
        schema: [] // no options
    },
    create: function (context) {
        return {
            Program: (node) => {
                const lineCount = context.getSourceCode().lines.length
                if (lineCount > 200) {
                    context.report({
                        node,
                        message: `File size (${lineCount} lines) exceeds 200 lines`
                    })
                }
            }
        }
    }
}
