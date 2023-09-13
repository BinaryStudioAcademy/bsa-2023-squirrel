import sys
import sqlglot

filename = sys.argv[1]
with open(filename) as data_file:
    sql = data_file.read()

def format_postgres(inputSql):
    if(has_syntax_error(inputSql) == False):
        return '\n'.join(result)
    else:
        return syntax_errors

def has_syntax_error(inputSql):
    try:
        global result 
        result = sqlglot.transpile(inputSql, read='postgres', pretty=True)
        return False
    except sqlglot.errors.ParseError as e:
        global syntax_errors
        syntax_errors = e.errors
        return True

print(has_syntax_error(sql))
print(format_postgres(sql))