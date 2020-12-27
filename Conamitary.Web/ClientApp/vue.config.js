// eslint-disable-next-line @typescript-eslint/no-var-requires
const path = require('path');

module.exports = {
    transpileDependencies: ['vuetify'],
    chainWebpack: config => {
        config.module
            .rule('ts')
            .exclude.add(/node_modules/)
            .end()
            .test(/\.ts$/)
            .use('ts-loader')
            .loader('ts-loader')
            .options({
                transpileOnly: true,
                /* It has to be in separate file. Otherwise it will not work */
                getCustomTransformers: path.resolve(
                    __dirname,
                    'vue-ts-nameof.js'
                ),
                appendTsSuffixTo: ['\\.vue$'],
                happyPackMode: true,
            })
            .end();
    },
    productionSourceMap: false,
};
