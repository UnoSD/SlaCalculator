const path = require("path");
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

let babelOptions = {
    presets: [
        ["@babel/preset-env", {
            "targets": {
                "browsers": ["last 2 versions"]
            },
            "modules": false,
            "useBuiltIns": "usage",
            "corejs": 3
        }]
    ],
};

let commonPlugins = [
    new HtmlWebpackPlugin({
        filename: './index.html',
        template: './index.html'
    })
];

module.exports = (env, options) => {
    // If no mode has been defined, default to `development`
    if (options.mode === undefined)
        options.mode = "development";

    let isProduction = options.mode === "production";
    console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

    return {
        devtool: 'inline-source-map',
        entry: isProduction ? // We don't use the same entry for dev and production, to make HMR over style quicker for dev env
            {
                demo: [
                    "@babel/polyfill",
                    './SlaCalculator.fsproj',
                    './main.scss'
                ]
            } : {
                app: [
                    "@babel/polyfill",
                    './SlaCalculator.fsproj'
                ],
                style: [
                    './main.scss'
                ]
            },
        output: {
            path: path.join(__dirname, './bin/publish'),
            filename: isProduction ? '[name].[hash].js' : '[name].js'
        },
        plugins: isProduction ?
            commonPlugins.concat([
                new MiniCssExtractPlugin({
                    filename: 'style.css'
                })
            ])
            : commonPlugins,
        devServer: {
            port: 8080,
            hot: true
        },
        module: {
            rules: [
                {
                    test: /\.fs(x|proj)?$/,
                    use: {
                        loader: "fable-loader",
                        options: {
                            babel: babelOptions
                        }
                    }
                },
                {
                    test: /\.js$/,
                    exclude: /node_modules/,
                    use: {
                        loader: 'babel-loader',
                        options: babelOptions
                    },
                },
                {
                    test: /\.(sass|scss|css)$/,
                    use: [
                        isProduction
                            ? MiniCssExtractPlugin.loader
                            : 'style-loader',
                        'css-loader',
                        'sass-loader',
                    ],
                },
                {
                    test: /\.css$/,
                    use: ['style-loader', 'css-loader']
                },
                {
                    test: /\.(png|jpg|jpeg|gif|svg|woff|ttf|eot)(\?.*$|$)/,
                    use: ["file-loader"]
                }
            ]
        }
    };
}
