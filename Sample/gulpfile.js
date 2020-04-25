const { src, dest, watch } = require('gulp');
const babel = require('gulp-babel');
const concat = require('gulp-concat');
const sourcemaps = require('gulp-sourcemaps');
const terser = require('gulp-terser');

exports.default = () =>
    src('wwwroot/js/*.js')
        .pipe(sourcemaps.init())
        .pipe(babel({
            presets: ['@babel/env']
        }))
        .pipe(concat('site.js'))
        .pipe(terser())
        .pipe(sourcemaps.write('.'))
        .pipe(dest('wwwroot/dist'))

exports.watch = () =>
    watch(['wwwroot/js/*.js'], exports.default);
